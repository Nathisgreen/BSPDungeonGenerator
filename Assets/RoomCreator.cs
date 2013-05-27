using UnityEngine;
using System.Collections;

public class RoomCreator : MonoBehaviour {
	
	private int roomID;
	
	private BSPNode parentNode;
	
	private GameObject sibiling;
	
	public void setup(){
		transform.position = new Vector3((int) transform.position.x, (int)transform.position.y, (int) transform.position.z);
		
		transform.position = new Vector3 (transform.position.x - (transform.localScale.x/2), transform.position.y, transform.position.z - (transform.localScale.z/2));
		
		for (int i = (int)transform.position.x; i < (int) transform.position.x + transform.localScale.x; i++){
			for (int j = (int)transform.position.z; j < (int) transform.position.z + transform.localScale.z; j++){
				BSPTree.setTile(i,j,1);
			}
		}
		
		for (int i = 0; i < transform.localScale.x+1; i++){
			BSPTree.setTile((int)transform.position.x + i,(int)transform.position.z,2);
			BSPTree.setTile((int)transform.position.x + i,(int)(transform.position.z + transform.localScale.z),2);
		}
		
		for (int i = 0; i < transform.localScale.z+1; i++){
			BSPTree.setTile((int)transform.position.x,(int)transform.position.z +i,2);
			BSPTree.setTile((int)(transform.position.x + transform.localScale.x) ,(int)transform.position.z + i,2);
		}
		
	}
	
	public void setID(int _aID){
		roomID = _aID;	
	}
	
	public void setParentNode(BSPNode _aNode){
		parentNode = _aNode;	
	}
	
	public void connect(){
		getSibiling();
		
		if (sibiling != null){
			
			Vector3 startPos = new Vector3();
			Vector3 endPos = new Vector3();
			
			if (sibiling.transform.position.z + sibiling.transform.localScale.z < transform.position.z){
				startPos = chooseDoorPoint(0);
				endPos = sibiling.GetComponent<RoomCreator>().chooseDoorPoint(2);
			}else if (sibiling.transform.position.z > transform.position.z + transform.localScale.z){
				startPos = chooseDoorPoint(2);
				endPos = sibiling.GetComponent<RoomCreator>().chooseDoorPoint(1);	
			}else if (sibiling.transform.position.x + sibiling.transform.localScale.x < transform.position.x){
				startPos = chooseDoorPoint(3);
				endPos = sibiling.GetComponent<RoomCreator>().chooseDoorPoint(1);	
			}else if(sibiling.transform.position.x > transform.position.x + transform.localScale.x){
				startPos = chooseDoorPoint(1);
				endPos = sibiling.GetComponent<RoomCreator>().chooseDoorPoint(3);
			}
			
			
			GameObject aDigger = (GameObject) Instantiate(Resources.Load("Digger"),startPos,Quaternion.identity);
			aDigger.GetComponent<Digger>().begin(endPos);
			
			
			parentNode = findRoomlessParent(parentNode);
				
			if (parentNode != null){
				
				int aC = Random.Range(0,2);
				
				if (aC == 0){
					parentNode.setRoom(this.gameObject);
				}else{
					parentNode.setRoom(sibiling.gameObject);
				}
				
				sibiling.GetComponent<RoomCreator>().setParentNode(parentNode);
			}

		}
		
	}
	
	private void getSibiling(){
		if (parentNode.getParentNode() != null){
			if (parentNode.getParentNode().getLeftNode() != parentNode){
				sibiling = parentNode.getParentNode().getLeftNode().getRoom();
			}else{
				sibiling = parentNode.getParentNode().getRightNode().getRoom();	
			}
		}
	}
	
	public Vector3 chooseDoorPoint(int _index){	
		switch (_index){
		case 0:
			return new Vector3((int)(transform.position.x + Random.Range(1,transform.localScale.x -2)),transform.position.y,(int)(transform.position.z));
		case 1:
			return new Vector3((int)(transform.position.x + transform.localScale.x),transform.position.y,(int)(transform.position.z + Random.Range(1,transform.localScale.z -2)));
		case 2:
			return new Vector3((int)(transform.position.x + Random.Range(1,transform.localScale.x -2)),transform.position.y,(int)(transform.position.z + transform.localScale.z));
		case 3:
			return new Vector3((int)(transform.position.x+1),transform.position.y,(int)(transform.position.z + Random.Range(1,transform.localScale.z -2)));
		default:
			return new Vector3(0,0,0);
		}
	}
	
	public BSPNode getParent(){
		return parentNode;	
	}
	
	public BSPNode findRoomlessParent(BSPNode _aNode){
		if (_aNode != null){
			if (_aNode.getRoom() == null){
				return _aNode;
			}else{
				return findRoomlessParent(_aNode.getParentNode());	
			}	
		}
		
		return null;
	}
}

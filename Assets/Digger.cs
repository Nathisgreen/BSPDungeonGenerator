using UnityEngine;
using System.Collections;

public class Digger : MonoBehaviour {
	
	private Vector3 targetPos;
	
	// Use this for initialization
	void Start () {

	}
	
	public void begin(Vector3 _targetPos){
		targetPos = _targetPos;
		
		dig();
	}
	
	private void updateTile(){
		BSPTree.setTile((int)transform.position.x,(int)transform.position.z, 1);
		BSPTree.setTile((int)transform.position.x+1,(int)transform.position.z, 1);
		BSPTree.setTile((int)transform.position.x-1,(int)transform.position.z, 1);
		BSPTree.setTile((int)transform.position.x,(int)transform.position.z+1, 1);
		BSPTree.setTile((int)transform.position.x,(int)transform.position.z-1, 1);
		
		surroundTilesWithWall((int)transform.position.x+1,(int)transform.position.z);
		surroundTilesWithWall((int)transform.position.x-1,(int)transform.position.z);
		surroundTilesWithWall((int)transform.position.x,(int)transform.position.z+1);
		surroundTilesWithWall((int)transform.position.x,(int)transform.position.z-1);

	}
	
	public void dig(){
		
		while(transform.position.x != targetPos.x){
			
			if (transform.position.x < targetPos.x){
				transform.position = new Vector3(transform.position.x +1, transform.position.y, transform.position.z);
			}else{
				transform.position = new Vector3(transform.position.x -1, transform.position.y, transform.position.z);	
			}
			
			updateTile();
		}
		
		while(transform.position.z != targetPos.z){
			if (transform.position.z < targetPos.z){
				transform.position = new Vector3(transform.position.x , transform.position.y, transform.position.z +1);
			}else{
				transform.position = new Vector3(transform.position.x , transform.position.y, transform.position.z -1);	
			}
			
			updateTile();
		}
		
		DestroyImmediate(this);	
	}
	
	public void surroundTilesWithWall(int _x, int _y){
		if (BSPTree.getGrid().getTile(_x+1,_y) == 0){
			BSPTree.setTile(_x+1,_y,2);
		}	
		
		if (BSPTree.getGrid().getTile(_x-1,_y) == 0){
			BSPTree.setTile(_x-1,_y,2);
		}
	
		if (BSPTree.getGrid().getTile(_x,_y+1) == 0){
			BSPTree.setTile(_x,_y+1,2);
		}
		
		if (BSPTree.getGrid().getTile(_x,_y-1) == 0){
			BSPTree.setTile(_x,_y-1,2);
		}
	}
	
}

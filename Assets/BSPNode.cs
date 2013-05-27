using UnityEngine;
using System.Collections;

public class BSPNode {
	
	
	GameObject cube;
	BSPNode parentNode;
	BSPNode leftNode;
	BSPNode rightNode;
	Color myColor;
	
	private bool isConnected = false;
	
	GameObject room;
	
	public BSPNode(){
		myColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
	}
	
	public void setLeftNode(BSPNode _aNode){
		leftNode = _aNode;
	}
	
	public void setRightNode(BSPNode _aNode){
		rightNode = _aNode;
	}
	
	public void setParentNode(BSPNode _aNode){
		parentNode = _aNode;	
	}
	
	public BSPNode getLeftNode(){
		return leftNode;
	}
	
	public BSPNode getRightNode(){
		return rightNode;
	}
	
	public BSPNode getParentNode(){
		return parentNode;	
	}
	
	void splitX(GameObject _aSection){
		
		float xSplit =  Random.Range(20,_aSection.transform.localScale.x-20);
		
		if (xSplit > 20){
			GameObject cube0 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube0.transform.localScale = new Vector3 (xSplit, _aSection.transform.localScale.y,_aSection.transform.localScale.z);
			cube0.transform.position = new Vector3(
				_aSection.transform.position.x - ((xSplit - _aSection.transform.localScale.x)/2),
				_aSection.transform.position.y,
				_aSection.transform.position.z);
			cube0.renderer.material.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
			cube0.tag = "GenSection";
			leftNode = new BSPNode();
			leftNode.setCube(cube0);
			leftNode.setParentNode(this);
			
			GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			float split1 = _aSection.transform.localScale.x - xSplit;
			cube1.transform.localScale = new Vector3 (split1, _aSection.transform.localScale.y,_aSection.transform.localScale.z);
			cube1.transform.position = new Vector3(
				_aSection.transform.position.x + ((split1 - _aSection.transform.localScale.x)/2),
				_aSection.transform.position.y,
				_aSection.transform.position.z);
			cube1.renderer.material.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
			cube1.tag = "GenSection";
			rightNode = new BSPNode();
			rightNode.setCube(cube1);
			rightNode.setParentNode(this);
			
			GameObject.DestroyImmediate(_aSection);
		}		
	}
	
	void splitZ(GameObject _aSection){
		float zSplit = Random.Range(20,_aSection.transform.localScale.z-20);
		float zSplit1 = _aSection.transform.localScale.z - zSplit;
		
		if (zSplit > 20){
		
			GameObject cube0 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube0.transform.localScale = new Vector3 (_aSection.transform.localScale.x, _aSection.transform.localScale.y,zSplit);
			cube0.transform.position = new Vector3(
				_aSection.transform.position.x,
				_aSection.transform.position.y,
				_aSection.transform.position.z - ((zSplit - _aSection.transform.localScale.z)/2));
			cube0.renderer.material.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
			cube0.tag = "GenSection";
			leftNode = new BSPNode();
			leftNode.setCube(cube0);	
			leftNode.setParentNode(this);
			
			GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube1.transform.localScale = new Vector3 (_aSection.transform.localScale.x, _aSection.transform.localScale.y,zSplit1);
			cube1.transform.position = new Vector3(
				_aSection.transform.position.x,
				_aSection.transform.position.y,
				_aSection.transform.position.z+ ((zSplit1 - _aSection.transform.localScale.z)/2));
			cube1.renderer.material.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
			cube1.tag = "GenSection";
			rightNode = new BSPNode();
			rightNode.setCube(cube1);
			rightNode.setParentNode(this);
			
			GameObject.DestroyImmediate(_aSection);
		}
	}
	
	public void setCube(GameObject _aCube){
		cube = _aCube;	
	}
	
	public GameObject getCube(){
		return cube;	
	}
	
	public void cut(){
		float choice = Random.Range(0,2);
		if (choice <=0.5){
			splitX (cube);	
		}else{
			splitZ(cube);	
		}
	}
	
	public Color getColor(){
		return myColor;	
	}
	
	public void setRoom(GameObject _aRoom){
		room = _aRoom;	
	}
	
	public GameObject getRoom(){
		return room;	
	}
	
	public void setConnected(){
		isConnected = true;	
	}
	
	public bool getIsConnected(){
		return isConnected;	
	}
	
}

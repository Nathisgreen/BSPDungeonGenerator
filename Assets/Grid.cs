using UnityEngine;
using System.Collections;

public class Grid {
	
	private int gridWidth;
	private int gridHeight;
	
	private int[,] grid;
	
	public Grid(int _width, int _height){
		gridWidth = _width;
		gridHeight = _height;
		
		grid = new int[gridWidth, gridHeight];
		
		for (int i = 0; i < gridWidth; i++){
			for (int j= 0; j < gridHeight; j++){
				grid[i,j] = 0;
			}
		}
	}
	
	public void setTile(int _x, int _y, int _value){
		grid[_x,_y] = _value;
	}
	
	public int getTile(int _x, int _y){
		return grid[_x,_y];	
	}
	
	public int Width(){
		return gridWidth;	
	}
	
	public int Height(){
		return gridHeight;	
	}
}

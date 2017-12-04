﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

public class TerrainCreator : MonoBehaviour
{
    public static List<DataPoint> places = new List<DataPoint>();

	// Use this for initialization
	void Awake () 
	{
		TxtReader("Assets/Scripts/terrain1.txt");
	}
	
	// Update is called once per frame
	void Update () {
        
	}

	// Method that reads terrain from txt file
	bool TxtReader(string fileName)
	{
		string line;
		int x = 0;

		try
		{
			int lineCount = File.ReadAllLines(fileName).Length;

			System.IO.StreamReader file = new System.IO.StreamReader(fileName);

			while((line = file.ReadLine()) != null)
			{
				foreach(char c in line)
				{
					if(c.Equals('T'))
					{
						Instantiate(Resources.Load("Prefabs/Trunk_Cube"), new Vector3(x, (float)lineCount, 1f), Quaternion.identity);
                        places.Add(new DataPoint((float)x,(float)lineCount));
					}
					if(c.Equals('H'))
					{ 
						Instantiate(Resources.Load("Prefabs/Leaf_Cube"), new Vector3(x, (float)lineCount, 1f), Quaternion.identity);
                        places.Add(new DataPoint((float)x,(float)lineCount));
					}
					x += 1;
				}
				x = 0;
				lineCount -= 1;
			}

			int lineLength = File.ReadAllLines(fileName)[0].Length;

			for(int z = 0; z < 2; z++)
			{
				for(x = 0; x < lineLength; x++)
				{
					Instantiate(Resources.Load("Prefabs/Floor_Cube"), new Vector3((float)x, 0f, (float)z), Quaternion.identity);
				}
			}
                
			return true;
		}
		catch(Exception e)
		{
			Debug.Log(e.Message);
			return false;
		}
	}

    public List<DataPoint> GetAvailablePlaces()
    {
        return places;
    }
}

public struct DataPoint
{
    public DataPoint(float x, float y)
    {
        x_coord = x;
        y_coord = y;
    }

    public float x_coord { get; private set; }
    public float y_coord { get; private set; }
}
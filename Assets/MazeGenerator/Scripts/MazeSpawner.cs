using UnityEngine;
using System.Collections;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour {
	public enum MazeGenerationAlgorithm{
		PureRecursive,
		RecursiveTree,
		RandomTree,
		OldestTree,
		RecursiveDivision,
	}

	public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
	public bool FullRandom = false;
	public int RandomSeed = 12345;
	public GameObject Floor = null;
	public GameObject Wall = null;
	public GameObject Pillar = null;
	public int Rows = 5;
	public int Columns = 5;
	public float CellWidth = 5;
	public float CellHeight = 5;
	public bool AddGaps = true;
	public GameObject GoalPrefab = null;
	public GameObject Chest = null;
	public GameObject Rock = null;
	public GameObject EnemySpawner = null;
	public int NumberOfSpawner;

	private BasicMazeGenerator mMazeGenerator = null;

	void Start () {
		if (!FullRandom) {
			//Random.seed = RandomSeed;
		}
		switch (Algorithm) {
		case MazeGenerationAlgorithm.PureRecursive:
			mMazeGenerator = new RecursiveMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveTree:
			mMazeGenerator = new RecursiveTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RandomTree:
			mMazeGenerator = new RandomTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.OldestTree:
			mMazeGenerator = new OldestTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveDivision:
			mMazeGenerator = new DivisionMazeGenerator (Rows, Columns);
			break;
		}
		mMazeGenerator.GenerateMaze ();
		for (int row = 0; row < Rows; row++) {
			for(int column = 0; column < Columns; column++){
				float x = column*(CellWidth+(AddGaps?.2f:0));
				float z = row*(CellHeight+(AddGaps?.2f:0));
				MazeCell cell = mMazeGenerator.GetMazeCell(row,column);
				GameObject tmp;
				tmp = Instantiate(Floor,new Vector3(x,0,z), Quaternion.Euler(0,0,0)) as GameObject;
				tmp.transform.parent = transform;
				int numberOfWallOn = 0;
				if(cell.WallRight){
					tmp = Instantiate(Wall,new Vector3(x+CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,90,0)) as GameObject;// right
					tmp.transform.parent = transform;
					numberOfWallOn++;
				}
				if(cell.WallFront){
					tmp = Instantiate(Wall,new Vector3(x,0,z+CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,0,0)) as GameObject;// front
					tmp.transform.parent = transform;
					numberOfWallOn++;
				}
				if(cell.WallLeft){
					tmp = Instantiate(Wall,new Vector3(x-CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,270,0)) as GameObject;// left
					tmp.transform.parent = transform;
					numberOfWallOn++;
				}
				if(cell.WallBack){
					tmp = Instantiate(Wall,new Vector3(x,0,z-CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;// back
					tmp.transform.parent = transform;
					numberOfWallOn++;
				}
				//if(cell.IsGoal && GoalPrefab != null){
				//	tmp = Instantiate(GoalPrefab,new Vector3(x,1,z), Quaternion.Euler(0,0,0)) as GameObject;
				//	tmp.transform.parent = transform;
				//}

				if (row == Rows - 1 && column == Columns - 1)
                {
					// Add Boss room
                }
				else if (row == 0 && column == Columns - 1)
                {
					// Add Buying room
                }
				else if (row == Rows - 1 && column == 0)
                {
					// Add Item room
                }
                else
                {
					if (numberOfWallOn >= 3)
					{
						// Instantiate Chest
						tmp = Instantiate(Chest, new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					}
				}

				int numberOfRock = Random.Range(0, 3);
				// Instantiate Rocks
				for (int index = 0; index < numberOfRock; index++)
                {
					float xRandom = Random.Range(-CellWidth / 2 + 1f, CellWidth/2 - 1f);
					float zRandom = Random.Range(-CellHeight/2 + 1f, CellHeight/2 - 1f);
					tmp = Instantiate(Rock, new Vector3(xRandom + x, 0, zRandom + z), Quaternion.Euler(0, 0, 0)) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}

		int[] rows = null; 
		int[] columns = null;
		GetRandomCellIndexes(NumberOfSpawner, out rows, out columns);
		for (int index = 0; index < NumberOfSpawner; index++)
        {
			float x = columns[index] * (CellWidth + (AddGaps ? .2f : 0));
			float z = rows[index] * (CellHeight + (AddGaps ? .2f : 0));
			float xRandom = Random.Range(-CellWidth / 2 + 1f, CellWidth / 2 - 1f);
			float zRandom = Random.Range(-CellHeight / 2 + 1f, CellHeight / 2 - 1f);
			GameObject tmp = Instantiate(EnemySpawner, new Vector3(xRandom + x, 0, zRandom + z), Quaternion.Euler(0, 0, 0)) as GameObject;
			tmp.transform.parent = transform;
		}


		if (Pillar != null){
			for (int row = 0; row < Rows+1; row++) {
				for (int column = 0; column < Columns+1; column++) {
					float x = column*(CellWidth+(AddGaps?.2f:0));
					float z = row*(CellHeight+(AddGaps?.2f:0));
					GameObject tmp = Instantiate(Pillar,new Vector3(x-CellWidth/2,0,z-CellHeight/2),Quaternion.identity) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}

		UnityEditor.AI.NavMeshBuilder.ClearAllNavMeshes();
		UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
	}

	private void GetRandomCellIndexes(int numberOfCell, out int[] rows, out int[] columns)
    {
		// Declare local variables
		rows = new int[numberOfCell];
		columns = new int[numberOfCell];
		int index = 0;
		bool retry = false;

		// Loop until all Enemy spawner are instantiated
		while (index != numberOfCell)
		{
			// Get random values
			int rowRandom = Random.Range(1, Rows);
			int columnRandom = Random.Range(1, Columns);

			// Loop in all indexes found so far
			foreach (int rowIndex in rows)
			{
				foreach (int columnIndex in columns)
				{
					// Check if the specific indexes are already found
					if (rowIndex == rowRandom && columnIndex == columnRandom)
					{
						// Get out the loop
						retry = true;
						break;
					}
				}
				
				if (retry == true)
                {
					break;
                }
			}

			if (retry != true)
            {
				rows[index] = rowRandom;
				columns[index] = columnRandom;
				index++;
			}

			retry = false;
		}
	}
}

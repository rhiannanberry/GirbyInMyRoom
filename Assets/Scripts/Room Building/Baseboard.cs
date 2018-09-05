using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baseboard : MonoBehaviour {
	Wall wall;
	MeshFilter meshFilter;
	Mesh prefabMesh;
	Mesh mesh;
	int prefVertexCount, meshVertexCount, endCapTriCount, tubeTriCount, triCount, triVertexCount, additionalLoopCount;
	Vector3[] prefVertices, meshVertices;	//sillohuette of the baseboard
	Vector2[] meshUVs;
	int[] meshTriangles;


	public void UpdateBaseboard() {
		/*
		 *
		 * Get relevant values and create necessary arrays
		 *
		 */

		wall = GetComponentInParent<Wall>();
		prefabMesh = wall.baseboard.GetComponent<MeshFilter>().sharedMesh;
		meshFilter = GetComponent<MeshFilter>();
		mesh = meshFilter.sharedMesh;

		prefVertexCount = prefabMesh.vertexCount;
		prefVertices = prefabMesh.vertices;	//sillohuette of the baseboard

		meshVertexCount = prefVertexCount*2;
		meshVertices = new Vector3[meshVertexCount];
		meshUVs = new Vector2[meshVertexCount];

		endCapTriCount = prefVertexCount-2;
		tubeTriCount = 2*(prefVertexCount-1);
		triCount = 2*endCapTriCount + tubeTriCount;
		triVertexCount = triCount*3;
		meshTriangles = new int[triVertexCount];

		additionalLoopCount = 1 + wall.doors.Length * 2;


		if (mesh == null) {
			mesh = new Mesh();
			meshFilter.mesh = mesh;
		}

		GenerateVerticesAndUV();
		Debugging();
		GenerateTriangles();

		/*
		 * Set all new mesh values and calculate normals
		 */

		mesh.vertices = meshVertices;
		mesh.triangles = meshTriangles;
		mesh.uv = meshUVs;
		mesh.RecalculateNormals();
		Debugging();
	}

	private void GenerateVerticesAndUV() {
		meshVertexCount = prefVertexCount + additionalLoopCount * prefVertexCount;
		meshVertices = new Vector3[meshVertexCount];
		meshUVs = new Vector2[meshVertexCount];

		Matrix4x4[] transMatrices = new Matrix4x4[additionalLoopCount];
		int loopCnt = 0;
		//door edge x positions
		for (int m = 0; m < wall.doors.Length; m++) {
			transMatrices[loopCnt++] = Matrix4x4.Translate(Vector3.right*wall.doors[m].transform.localPosition.x);
			transMatrices[loopCnt++] = Matrix4x4.Translate(Vector3.right*(wall.doors[m].transform.localPosition.x + wall.doors[m].width));
		}

		//right end of wall
		transMatrices[loopCnt++] = Matrix4x4.Translate(Vector3.right*(wall.width));

		for (int i = 0; i < prefVertexCount; i++) {
			meshVertices[i] = prefVertices[i];
			meshUVs[i] = prefVertices[i];
			
			int transCnt = 0;
			foreach (Matrix4x4 matrix in transMatrices) {
				transCnt += prefVertexCount;
				Vector3 translation = matrix.MultiplyPoint3x4(prefVertices[i]);
				meshVertices[i + transCnt] = translation;
				meshUVs[i + transCnt] = translation;
			}	
		}
	}

	private void GenerateTriangles() {
		triCount = 2*endCapTriCount + 6*(wall.doors.Length+1);
		triVertexCount = 3*triCount;

		Debug.Log("triVertexCount = " + triVertexCount);
		
		meshTriangles = new int[triVertexCount];

		int currentTriVertex = 0;

		//left end cap
		for (int i = 0; i < prefVertexCount - 2; i++) {
			meshTriangles[currentTriVertex++] = i+1;
			meshTriangles[currentTriVertex++] = i+2;
			meshTriangles[currentTriVertex++] = 0;
		}

		//tube
		for (int j = 0; j < additionalLoopCount + 1; j++) {
			
			if (j%2 == 0) {
				Debug.Log("Loop Strip");
				int off = j*prefVertexCount;
				for (int k = 0; k < prefVertexCount-1; k++) {
					
					//back triangle
					meshTriangles[currentTriVertex++] = off + k + prefVertexCount;
					meshTriangles[currentTriVertex++] = off + k;
					meshTriangles[currentTriVertex++] = off + k + 1;

					//front triangle
					meshTriangles[currentTriVertex++] = off + k + prefVertexCount;
					meshTriangles[currentTriVertex++] = off + k + 1;
					meshTriangles[currentTriVertex++] = off + k + prefVertexCount + 1;
					Debug.Log("Furthest Vert: " + (off + k + prefVertexCount + 1));
				}
			}
		}

		//right end cap
		int rightCapStart = meshVertexCount - prefVertexCount;
		for (int k = 0; k < prefVertexCount - 2; k++) {
			meshTriangles[currentTriVertex++] = rightCapStart;
			meshTriangles[currentTriVertex++] = rightCapStart + k+1;
			meshTriangles[currentTriVertex++] = rightCapStart + k+2;
		}

	}

	private void Debugging() {
		Debug.Log("VERTEX COUNT: " + meshVertexCount);
		if (wall.doors.Length > 0) {
			Debug.Log("DOOR BOTTOM LEFT: " + wall.doors[0].transform.localPosition.x);
			Debug.Log("DOOR BOTTOM RIGHT: " + wall.doors[0].transform.localPosition.x + wall.doors[0].width);

			Debug.DrawLine(Vector3.down, Vector3.right*(wall.doors[0].transform.localPosition.x + wall.doors[0].width)*wall.transform.localScale.x, Color.red, 0);
		}
	}
	
}

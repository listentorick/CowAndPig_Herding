using UnityEngine;
using System.Collections;

public interface IPathManager {	
	 event PathStart PathStart;
	 event PathEnd PathEnd;
	 event PointAdded PointAdded;
}

public delegate void PointAdded(IPathManager sender, Vector3 point);
public delegate void PathStart(IPathManager sender, Vector3 point);
public delegate void PathEnd(IPathManager sender, Vector3 point);
public delegate void PointCancelled(IPathManager sender);
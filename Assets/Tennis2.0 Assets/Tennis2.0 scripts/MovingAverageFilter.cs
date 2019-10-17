using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAverageFilter
{
	int currentIndex = 0;
    float currentWindowSum = 0;
    float lastInsertionNumber = 0;
    const int windowSize = 3;
    float[] window = new float[windowSize];

	public float filter(float z)
	{		
        float oldestWindowValue = window[currentIndex];
        currentWindowSum = currentWindowSum - oldestWindowValue + z;
        window[currentIndex] = z;
        currentIndex = (currentIndex + 1) % windowSize;
       
       // System.out.println(currentWindowSum/WINDOW_SIZE);

        return currentWindowSum/windowSize;
	}
}

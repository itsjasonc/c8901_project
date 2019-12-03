using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Code based on the pseudocode provided in the textbook.
 * Artificial Intelligence for Games by Ian Millington (1st edition)
 */
public class NaiveBayesClassifier
{
    private const int NUM_ATTRIBUTES = 3;
    // Number of positive examples, none initially
    public int examplesCountPositive = 0;
    // Number of negative examples, none initially
    public int examplesCountNegative = 0;

    // Number of times each attribute was true for the
    // positive examples, initially all zero
    public int[] attributeCountsPositive = new int[NUM_ATTRIBUTES]
    {
        0, 0, 0
    };

    // Number of times each attribute was true for the
    // negative examples, initially all zero
    public int[] attributeCountsNegative = new int[NUM_ATTRIBUTES]
    {
        0, 0, 0
    };

    public void update(ref List<bool> attributes, bool label)
    {
        // Check if this is a positive or negative example,
        // update all the counts accordingly
        if (label)
        {
            // using element-wise addition
            for (int i = 0; i < attributeCountsPositive.Length; i++)
            {
                if (attributes[i])
                {
                    attributeCountsPositive[i]++;
                    examplesCountPositive += 1;
                }
            }
        }
        else
        {
            // attributeCountsNegative += attributes;
            examplesCountNegative += 1;
        }
    }

    public bool predict(ref List<bool> attributes)
    {
        // predict must label this example as positive
        // or negative example
        float x = naiveProbabilities(ref attributes,
                                    attributeCountsPositive,
                                    examplesCountPositive,
                                    examplesCountNegative);
        float y = naiveProbabilities(ref attributes,
                                    attributeCountsPositive,
                                    examplesCountPositive,
                                    examplesCountNegative);

        if (x >= y)
            return true;
        return false;
    }

    public float naiveProbabilities(ref List<bool> attributes, int[] counts, int m, int n)
    {
        // Compute the prior
        float prior = m / (m + n);

        // Naive assumption of conditional independence
        float p = 1.0f;

        for (int i = 0; i < NUM_ATTRIBUTES; i++)
        {
            p /= m;
            if (attributes[i])
                p *= counts[i];
            else
                p *= m - counts[i];
        }

        return prior * p;
    }
}

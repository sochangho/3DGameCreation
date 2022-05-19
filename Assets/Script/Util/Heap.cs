
using System.Collections.Generic;
using System;


public abstract class Heap
{

   abstract public void Add(TemporaryData data);
    abstract public TemporaryData RemoveOne();
  
}

public class MaxHeap:Heap
{
    private List<TemporaryData> A = new List<TemporaryData>();

    override public void Add(TemporaryData data)
    {
        // add at the end
        A.Add(data);

        // bubble up
        int i = A.Count - 1;
        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (A[parent].value < A[i].value) // MinHeap에선 반대
            {
                Swap(parent, i);
                i = parent;
            }
            else
            {
                break;
            }
        }
    }

    override public TemporaryData RemoveOne()
    {
        if (A.Count == 0)
            throw new InvalidOperationException();

        TemporaryData root = A[0];

        // move last to first 
        // and remove last one
        A[0] = A[A.Count - 1];
        A.RemoveAt(A.Count - 1);

        // bubble down
        int i = 0;
        int last = A.Count - 1;
        while (i < last)
        {
            // get left child index
            int child = i * 2 + 1;

            // use right child if it is bigger                
            if (child < last &&
                A[child].value < A[child + 1].value) // MinHeap에선 반대
                child = child + 1;

            // if parent is bigger or equal, stop
            if (child > last ||
               A[i].value >= A[child].value) // MinHeap에선 반대
                break;

            // swap parent/child
            Swap(i, child);
            i = child;
        }

        return root;
    }

    private void Swap(int i, int j)
    {
        TemporaryData t = A[i];
        A[i] = A[j];
        A[j] = t;
    }
}



public class MinHeap : Heap
{
    private List<TemporaryData> A = new List<TemporaryData>();

    override public void Add(TemporaryData data)
    {
        // add at the end
        A.Add(data);

        // bubble up
        int i = A.Count - 1;
        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (A[parent].value > A[i].value) // MinHeap에선 반대
            {
                Swap(parent, i);
                i = parent;
            }
            else
            {
                break;
            }
        }
    }

    override public TemporaryData RemoveOne()
    {
        if (A.Count == 0)
            throw new InvalidOperationException();

        TemporaryData root = A[0];

        // move last to first 
        // and remove last one
        A[0] = A[A.Count - 1];
        A.RemoveAt(A.Count - 1);

        // bubble down
        int i = 0;
        int last = A.Count - 1;
        while (i < last)
        {
            // get left child index
            int child = i * 2 + 1;

            // use right child if it is bigger                
            if (child < last &&
                A[child].value > A[child + 1].value) // MinHeap에선 반대
                child = child + 1;

            // if parent is bigger or equal, stop
            if (child > last ||
               A[i].value <= A[child].value) // MinHeap에선 반대
                break;

            // swap parent/child
            Swap(i, child);
            i = child;
        }

        return root;
    }

    private void Swap(int i, int j)
    {
        TemporaryData t = A[i];
        A[i] = A[j];
        A[j] = t;
    }
}
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{
    public static Dictionary<int,int> sums = new Dictionary<int,int>();
    /*
     * Complete the 'cutTheTree' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY data
     *  2. 2D_INTEGER_ARRAY edges
     */

    public static int cutTheTree(int numVertices,List<int> data, List<List<int>> edges)
    {
        

        //loop vertices
        
        var minAbsolute = calcSumNode(1,data,edges);

        foreach(List<int> edge in edges)
        {
            var absolute = calcAbsoluteDifference(edge);
            if(absolute < minAbsolute)
                minAbsolute =  absolute;
        }            

        return minAbsolute;
    }

    public static int calcSumNode(int node,List<int> data, List<List<int>> edges)
    {
        var sum = 0;
        var children = edges.Where(x => x[0] == node).ToList();
        foreach(List<int> child in children)
        {
            sum += calcSumNode(child[1],data,edges);
        }
        sum += data[node-1];
        sums[node] = sum;
        return sum; 
    }

    public static int calcAbsoluteDifference(List<int> cutEdge)
    {
        var total = sums[1];
        var cutEdgeTotal = sums[cutEdge[1]];

        var absolute = Math.Abs(cutEdgeTotal - (total - cutEdgeTotal));

        return absolute;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> data = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(dataTemp => Convert.ToInt32(dataTemp)).ToList();

        List<List<int>> edges = new List<List<int>>();

        for (int i = 0; i < n - 1; i++)
        {
            edges.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(edgesTemp => Convert.ToInt32(edgesTemp)).ToList());
        }

        int result = Result.cutTheTree(n,data, edges);

        Console.WriteLine(result);
        Console.ReadLine();
    }
}

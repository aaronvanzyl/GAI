﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTreeRenderer : MonoBehaviour
{
    public NodeRenderer nodeRendererPrefab;
    public bool alignHorizontal = true;
    public float nodeWidth = 1;
    public float nodeHeight = 1;


    public void RenderTree(Node rootNode) {
        rootNode.CalculateWidth();
        RenderTreeRecursive(rootNode, Vector3.zero);
    }

    void RenderTreeRecursive(Node node, Vector3 position)
    {
        Debug.Log($"rendering node: in: {node.inNodes.Count} out: {node.outNode != null}");
        NodeRenderer renderer = Instantiate(nodeRendererPrefab, position, Quaternion.identity, transform);
        renderer.SetNode(node);

        float offset = -node.width * 0.5f;
        for (int i = 0; i < node.inNodes.Count; i++) {
            float mainAxisOffset = alignHorizontal ? -nodeWidth : - nodeHeight;
            float orthogonalOffset = (offset + node.inNodes[i].width * 0.5f) * (alignHorizontal ? nodeHeight : nodeWidth);
            Vector3 childPos = new Vector3(position.x +( alignHorizontal ? mainAxisOffset : orthogonalOffset), position.y + (alignHorizontal ? orthogonalOffset : mainAxisOffset), position.z);
            RenderTreeRecursive(node.inNodes[i], childPos);
            offset += node.inNodes[i].width;
        }
    }

    public void Clear() { 
        
    }
}

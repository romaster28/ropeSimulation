using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeLineRendererVisualizer : MonoBehaviour
{
    [SerializeField] private RopeConfigSerializable _config;
    
    private RopeSimulation _simulation;
    private LineRenderer _renderer;

    public void SetRope(RopeSimulation ropeSimulation)
    {
        _simulation = ropeSimulation ?? throw new ArgumentNullException(nameof(ropeSimulation));
    }

    private void Awake()
    {
        _renderer = GetComponent<LineRenderer>();
        SetRope(new RopeSimulation(_config, transform.position));
    }

    private void FixedUpdate()
    {
        _simulation.BlockSegment(0, transform.position);
        _simulation?.Simulate();
    }
    
    private void LateUpdate()
    {
        _renderer.positionCount = _simulation.SegmentsCount;
        
        int index = 0;
        
        foreach (var segment in _simulation.GetSegments())
        {
            _renderer.SetPosition(index, segment);
            index++;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class MapProceduralGenerator : MonoBehaviour
{
    [SerializeField] private SpriteShapeController _spriteShapeController;

    [SerializeField, Range(20f, 200f)] private int _levelLength = 20;  // numer of points for curves
    [SerializeField, Range(1f, 50f)] private float _xMultiplier = 2f;    // length for X axis
    [SerializeField, Range(1f, 50f)] private float _yMultiplier = 2f;    // length for Y axis
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;    // Smooth param for curves
    
    public float _noiseStep = 0.5f;
    public float _bottom = 10f;

    private Vector3 _lastPos;

    private void OnValidate() {

        _spriteShapeController.spline.Clear();  // clearing the map

        for(int i=0; i < _levelLength; i++) {
            _lastPos = transform.position + new Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier); // creating the point
            _spriteShapeController.spline.InsertPointAt(i, _lastPos); // adding the point to the map (spline)

            if(i!=0 && i!=_levelLength - 1) {
                // if its not the start and not the end of map, create curves between points
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness);                
            }
        }
        // creating the edges for map (two on the bottom)
        _spriteShapeController.spline.InsertPointAt(_levelLength, new Vector3(_lastPos.x, transform.position.y - _bottom));
        _spriteShapeController.spline.InsertPointAt(_levelLength + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
    }

// Not neccessary
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}

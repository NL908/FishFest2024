using UnityEngine;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
/// </summary>
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class LockCamera : CinemachineExtension
{
    [Tooltip("This is the starting Y position of the camera")]
    public float minYpos = 0f;

    [Tooltip("This is the y pos of the surface, so the camera doesn't move up anymore")]
    [HideInInspector]
    public float maxYpos = 1000f;
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (enabled && stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = 0;
            if (pos.y > minYpos && pos.y < maxYpos)
            {
                minYpos = pos.y;
            }
            else if (pos.y >= maxYpos)
            {
                pos.y = maxYpos;
            }
            else
            {
                pos.y = minYpos;
            }
            state.RawPosition = pos;
        }
    }
}
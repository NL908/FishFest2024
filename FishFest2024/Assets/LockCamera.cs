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
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (enabled && stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = 0;
            if (pos.y > minYpos)
            {
                minYpos = pos.y;
            }
            else
            {
                pos.y = minYpos;
            }
            state.RawPosition = pos;
        }
    }
}
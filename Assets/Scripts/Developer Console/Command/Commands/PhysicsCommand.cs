using Console.Command;
using UnityEngine;

namespace Console.Extras
{
    public static class PhysicsCommand
    {
        [Command]
        private static void SetGravity2DY(float value)
        {
            Physics2D.gravity = new Vector3(Physics.gravity.x, value);
        }

        [Command]
        private static void SetGravity2D(Vector3 gravity)
        {
            Physics2D.gravity = gravity;
        }

        [Command]
        private static void SetGravityY(float value)
        {
            Physics.gravity = new Vector3(Physics.gravity.x, value, Physics.gravity.z);
        }

        [Command]
        private static void SetGravity(Vector3 gravity)
        {
            Physics.gravity = gravity;
        }
    }
}
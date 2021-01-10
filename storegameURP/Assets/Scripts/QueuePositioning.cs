using UnityEngine;

public static class QueuePositioning
{
    private const float LineStartOffset = 1.4f;
    private const float IndividualOffset = 2;
    private const int MaxCheckAngle = 180;
    private const int CheckInterval = 1;

    private const float CheckRadius = 0.5f;

    public static Vector3[] GenerateQueue(Register reg, int length)
    {
        if (length == 0) throw new System.ArgumentOutOfRangeException("length", "Queue length cannot be zero.");

        Vector3[] positions = new Vector3[length];

        positions[0] = reg.transform.position - reg.transform.forward * LineStartOffset;

        // On the second position and up...
        Vector3 forward;
        for (int i = 1; i < length; i++)
        {
            if (i == 1)
            { forward = -reg.transform.forward * IndividualOffset; }
            // Forward not multiplied since the distance between customers is always equal to IndividualOffset.
            else
            { forward = positions[i - 1] - positions[i - 2]; }

            positions[i] = positions[i - 1] + forward;

            // Check for obstacles.
            if (OverlapSphere(positions[i]) || BlockedFrom(positions[i - 1], positions[i]))
            {
                Vector3 pos = positions[i];
                positions[i] = FindPositionAround(positions[i - 1], positions[i]);

                // If the value didn't change, log a warning.
                if (pos == positions[i])
                { Debug.LogWarning($"Couldn't find a suitable queue position at index {i} at register {reg.gameObject.name}. Skipping..."); }
            }
        }

        return positions;
    }

    private static bool OverlapSphere(Vector3 pos)
    {
        Collider[] collided = new Collider[1];
        return Physics.OverlapSphereNonAlloc(pos, CheckRadius, collided, ~LayerMask.GetMask("Player")) > 0;
    }

    private static bool BlockedFrom(Vector3 from, Vector3 to)
    {
        Vector3 delta = (to - from).normalized;
        return Physics.Raycast(from, delta, IndividualOffset, ~LayerMask.GetMask("Player"));
    }

    private static Vector3 FindPositionAround(Vector3 lastPosition, Vector3 position)
    {
        Vector3 forward = position - lastPosition;

        // Checks towards the left first if coin flip is heads.
        int side = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector3 attempt = new Vector3();

        if (Spin(side) || Spin(-side)) return attempt;

        // If spinning in both directions failed, return the original value.
        return position;

        bool Spin(int multiplier)
        {
            for (int angle = 0; angle <= MaxCheckAngle / 2; angle += CheckInterval)
            {
                attempt = lastPosition + Quaternion.Euler(0, multiplier * angle, 0) * forward;
                if (!OverlapSphere(attempt) && !BlockedFrom(lastPosition, attempt)) return true;
            }
            return false;
        }
    }
}

using UnityEngine;

public class MoveUp : Command
{
    //Rigidbody m_rb;
    float m_jumpForce;

    public MoveUp(Rigidbody rb, float jumpForce)
    {
        m_rb = rb;
        m_jumpForce = jumpForce;
    }

    public override void Execute()
    {
        // Set time stamp to the correct time
        timeStamp = Time.timeSinceLevelLoad;
        // Add sideways force
        m_rb.AddForce(0, m_jumpForce * Time.deltaTime, 0, ForceMode.VelocityChange);
    }
}

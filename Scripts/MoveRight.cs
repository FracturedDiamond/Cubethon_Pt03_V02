using UnityEngine;

class MoveRight : Command
{
    //Rigidbody m_rb;
    float m_sidewaysForce;

    public MoveRight(Rigidbody rb, float sidewaysForce)
    {
        m_rb = rb;
        m_sidewaysForce = sidewaysForce;
    }

    public override void Execute()
    {
        // Set time stamp to the correct time
        timeStamp = Time.timeSinceLevelLoad;
        // Add sideways force
        m_rb.AddForce(m_sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}
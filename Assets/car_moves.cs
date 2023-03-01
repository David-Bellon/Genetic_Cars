using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;

public class car_moves : MonoBehaviour
{
    public float speed = 2;
    public float rotation_factor = 0.22f;
    public float score = 0;
    public bool isDead;
    private left_sensor l_sensor;
    private left_up_sensor l_u_sensor;
    private middle_sensor m_sensor;
    private right_sensor r_sensor;
    private right_up_sensor r_u_sensor;
    private Master_Game mg;
    public int car_number;
    private IWorker worker;
    public string filename;
    private Tensor tensor;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        l_sensor = gameObject.GetComponentInChildren<left_sensor>();
        l_u_sensor = gameObject.GetComponentInChildren<left_up_sensor>();
        m_sensor = gameObject.GetComponentInChildren<middle_sensor>();
        r_sensor = gameObject.GetComponentInChildren<right_sensor>();
        r_u_sensor = gameObject.GetComponentInChildren<right_up_sensor>();
        isDead = false;
        score = 0;
        mg = transform.parent.GetComponent<Master_Game>();
        car_number = mg.numberCar;
        filename = "individual_" + car_number.ToString();
        var model = ModelLoader.Load((NNModel)Resources.Load(filename));
        worker = WorkerFactory.CreateWorker(model, WorkerFactory.Device.GPU);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            tensor = new Tensor(1, 1, 1, 5);
            tensor[0] = l_sensor.distance;
            tensor[1] = l_u_sensor.distance;
            tensor[2] = m_sensor.distance;
            tensor[3] = r_sensor.distance;
            tensor[4] = r_u_sensor.distance;
            score = score + Time.deltaTime;
            float y = worker.Execute(tensor).PeekOutput()[0];
            if (y > 0)
            {
                transform.Rotate(0.0f, 0.0f, -rotation_factor * y);
            }
            if (y < 0)
            {
                transform.Rotate(0.0f, 0.0f, -rotation_factor * y);
            }
            Vector2 movement = new Vector2(1, 0);
            transform.Translate(movement * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isDead = true;
    }

    public void OnDestroy()
    {
        Debug.Log(car_number);
        worker?.Dispose();
        tensor?.Dispose();
        //System.Threading.Thread.Sleep(400);
    }
}

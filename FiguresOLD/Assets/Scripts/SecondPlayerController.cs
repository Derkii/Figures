using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerController : MonoBehaviour
{

    //Время, за которое персонаж пройдет расстояние от 1 точки до другой
    public float TimeToMovePointToPoint  = 0.1f;

    [Range(0,10)] public float DelayBeforeChangeFigure = 2f;
    private MeshRenderer MeshRenderer;
    public List<GameObject> Primitives = new List<GameObject>();

    [HideInInspector] public Vector3[] Points;

    public Vector3[] StarPoints;

    public Vector3[] HexagonPoints;

    public float DelayBeforeNextPointTime =  0.1f;

    public Material MaterialForPrimitives;

    private TrailRenderer trailRenderer;



    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.time = 0f;


        trailRenderer.time = 10f;

        MeshRenderer = GetComponent<MeshRenderer>();

    }
    private void Start()
    {
        StartCoroutine(StartMove(MeshRenderer,trailRenderer));

    }
    
    IEnumerator Move(Vector3 startPos, Vector3 endPos, float time)
    {

        var currentTime = 0f;
        while (currentTime < time)
        {

            transform.position = Vector3.Lerp(startPos, endPos, 1 - (time - currentTime) / time);
            currentTime += UnityEngine.Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;

        GameObject NewPrimitive = GameObject.CreatePrimitive(PrimitiveType.Cube);

        NewPrimitive.transform.position = endPos;

        NewPrimitive.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        MeshRenderer NewPrimitiveMeshRenderer = NewPrimitive.GetComponent<MeshRenderer>();

        NewPrimitiveMeshRenderer.material = MaterialForPrimitives;
        NewPrimitiveMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;


        Primitives.Add(NewPrimitive);



        yield return null;


    }
    IEnumerator PrimitivesClear(List<GameObject> Primitives)
    {
        for (int i = 0; i < Primitives.Count; i++)
        {
            Destroy(Primitives[i]);

        }
        Primitives.Clear();
        yield return null;
    }
    IEnumerator Move(Vector3[] PointsForMove, float Delay)
    {
        StartCoroutine(PrimitivesClear(Primitives));

        Points = PointsForMove;
        for (int i = 0; i < Points.Length; i++)
        {
            Vector3 tarPos;

            tarPos = Points[i];

            yield return Move(transform.position, tarPos, this.TimeToMovePointToPoint);
            yield return new WaitForSeconds(Delay);


        }
        yield return Move(transform.position, Points[0], this.TimeToMovePointToPoint);
        yield return new WaitForSeconds(Delay);




    }

    IEnumerator StartMove(MeshRenderer renderer,TrailRenderer trailRenderer)
    {

        while (true)
        {

            yield return Move(HexagonPoints, DelayBeforeNextPointTime);

            renderer.enabled = false;

            yield return new WaitForSeconds(DelayBeforeChangeFigure);

            trailRenderer.time = 0;

            yield return new WaitForSeconds(0.1f);
            
            trailRenderer.time = 15;

            renderer.enabled = true;


            yield return Move(StarPoints, DelayBeforeNextPointTime);


            renderer.enabled = false;

            yield return new WaitForSeconds(DelayBeforeChangeFigure);
            trailRenderer.time = 0;

            yield return new WaitForSeconds(0.1f);

            trailRenderer.time = 15;

            renderer.enabled = true;


        }
    }


    }


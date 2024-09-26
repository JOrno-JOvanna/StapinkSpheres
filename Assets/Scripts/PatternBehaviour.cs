using System.Collections;
using UnityEngine;

public class PatternBehaviour: MonoBehaviour 
{
    public DelegateHolder delegateHolder;
    public bool getBack;

    private Vector3 origPosition, endPosition;

    private void OnEnable()
    {
        if (getBack)
        {
            delegateHolder.SetPatternMenu += MovePattern;
        }
        else
        {
            delegateHolder.SetPatternGame += MovePattern;
        }
    }

    private void Start()
    {
        origPosition = transform.position;
        endPosition = transform.position;
    }

    private void MovePattern()
    {
        origPosition = transform.position;

        endPosition = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);

        if (getBack)
        {
            StartCoroutine(GetPatternBack(1));
        }
    }

    private IEnumerator GetPatternBack(float time)
    {
        if(time > 0)
        {
            yield return new WaitForSeconds(1);

            endPosition = origPosition;
        }
        else
        {
            endPosition = origPosition;
        }
    }

    public void Update()
    {
        transform.position = Vector3.Lerp(transform.position, endPosition, 9 * Time.deltaTime);
    }

    public void SetOriginalPosition()
    {
        StartCoroutine(GetPatternBack(0));
    }

    private void OnDisable()
    {
        if (getBack)
        {
            delegateHolder.SetPatternMenu -= MovePattern;
        }
        else
        {
            delegateHolder.SetPatternGame -= MovePattern;
        }
    }
}

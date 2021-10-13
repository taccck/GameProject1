using System.Collections;
using FG;
using UnityEngine;
using UnityEngine.InputSystem;

public class CookingController : MonoBehaviour
{
    [Header("Event"), SerializeField] private float time;
    [SerializeField] private Progressbar progBar;
    [SerializeField] private Progresscircle progCircle;
    [SerializeField] private Stayinzone progZone;
    [SerializeField] private Clickzone progClick;

    [Header("Animation"), SerializeField] private Sprite success;
    [SerializeField] private Sprite fail;
    [SerializeField] private SpriteRenderer toSwap;
    [SerializeField] private SpriteRenderer toHide;
    [SerializeField] private Animator toStop;

    private enum EventType
    {
        Bar,
        Circle,
        Zone,
        Click
    }

    private EventType eventType;
    private Vector2 correctInput;
    private CookingInputDisplay display;

    private void OnQTE(InputValue value)
    {
        Vector2 buttonsPressed = value.Get<Vector2>();
        if (buttonsPressed == Vector2.zero) return;

        if (CheckInput(buttonsPressed))
        {
            AddScore();
        }
    }

    private bool CheckInput(Vector2 input)
    {
        bool correctX = Mathf.Approximately(correctInput.x, input.x);
        bool correctY = Mathf.Approximately(correctInput.y, input.y);
        bool correct = correctX && correctY;
        NewCorrectInput();

        return correct;
    }

    private void NewCorrectInput()
    {
        switch (eventType)
        {
            case EventType.Bar:
                correctInput = progBar.Checkinput();
                break;
            case EventType.Circle:
                correctInput = progCircle.Checkinput();
                break;
            case EventType.Zone:
                correctInput = progZone.Checkinput();
                break;
            case EventType.Click:
                correctInput = progClick.Checkinput();
                break;
        }

        display.SetUI(correctInput);
    }

    private void AddScore()
    {
        switch (eventType)
        {
            case EventType.Bar:
                progBar.Interact();
                break;
            case EventType.Circle:
                progCircle.Interact();
                break;
            case EventType.Zone:
                progZone.Interact();
                break;
            case EventType.Click:
                progClick.Interact(Vector2.zero);
                break;
        }
    }

    public void Outcome(bool outcome)
    {
        if (toHide != null)
            toHide.color = new Color(0, 0, 0, 0);
        if (toStop != null)
            toStop.enabled = false;

        if (fail == null || success == null) return;

        toSwap.sprite = outcome ? success : fail;
    }

    private IEnumerator CookTime()
    {
        yield return new WaitForSeconds(time);

        switch (eventType)
        {
            case EventType.Bar:
                Outcome(progBar.Isfilled());
                break;
            case EventType.Circle:
                Outcome(progCircle.Isfilled());
                break;
            case EventType.Zone:
                Outcome(progZone.Isfilled());
                break;
            case EventType.Click:
                Outcome(progClick.Isfilled());
                break;
        }
    }

    private void Start()
    {
        StartCoroutine(CookTime());
        if (progBar != null)
        {
            eventType = EventType.Bar;
            progBar.Startbar();
        }
        else if (progCircle != null)
        {
            eventType = EventType.Circle;
            progCircle.Startbar();
        }
        else if (progZone != null)
        {
            eventType = EventType.Zone;
            progZone.Startbar();
        }
        else if (progClick != null)
        {
            eventType = EventType.Click;
            progClick.Startbar();
        }

        NewCorrectInput();
    }

    private void Awake()
    {
        display = GetComponentInChildren<CookingInputDisplay>();
    }
}
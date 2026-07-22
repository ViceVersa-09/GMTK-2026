using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] Sprite outlinedSprite;
    [SerializeField] UnityEvent onClick;

    Sprite ogSprite;
    SpriteRenderer spriteRenderer;
    InputAction click;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        click = InputSystem.actions.FindAction("Click");

        ogSprite = spriteRenderer.sprite;
    }

    private void Update()
    {
        CheckMouse();
    }

    void CheckMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            spriteRenderer.sprite = outlinedSprite;

            if (click.WasPressedThisFrame())
            {
                onClick.Invoke();
            }
        }
        else
        {
            spriteRenderer.sprite = ogSprite;
        }
    }
}

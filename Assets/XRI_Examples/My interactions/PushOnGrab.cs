using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Пространство имён XRI

// Требуем наличие компонента XRGrabInteractable на объекте
[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class PushOnGrab : MonoBehaviour
{
    public float pushForce = 3.0f; // Сила толчка, настраивается в инспекторе
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Подписываемся на событие начала захвата
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDestroy()
    {
        // Отписываемся, чтобы не было утечек
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // args.interactorObject - это контроллер, который схватил объект.
        // Получаем направление "вперёд" от контроллера и толкаем объект.
        if (args.interactorObject != null && rb != null)
        {
            Vector3 pushDirection = args.interactorObject.transform.forward;
            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
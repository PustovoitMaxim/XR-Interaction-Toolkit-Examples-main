using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
[RequireComponent(typeof(MeshRenderer))]
public class ColorController : MonoBehaviour
{
    public Material defaultMaterial;
    public Material hoverMaterial;
    public Material selectMaterial;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private MeshRenderer meshRenderer;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        meshRenderer = GetComponent<MeshRenderer>();

        // Подписываемся на события
        grabInteractable.hoverEntered.AddListener(OnHoverEnter);
        grabInteractable.hoverExited.AddListener(OnHoverExit);
        grabInteractable.selectEntered.AddListener(OnSelectEnter);
        grabInteractable.selectExited.AddListener(OnSelectExit);

        // Устанавливаем начальный цвет
        meshRenderer.material = defaultMaterial;
    }

    void OnDestroy()
    {
        // Отписываемся
        grabInteractable.hoverEntered.RemoveListener(OnHoverEnter);
        grabInteractable.hoverExited.RemoveListener(OnHoverExit);
        grabInteractable.selectEntered.RemoveListener(OnSelectEnter);
        grabInteractable.selectExited.RemoveListener(OnSelectExit);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        // Если объект уже захвачен, не меняем цвет на hover
        if (!grabInteractable.isSelected)
            meshRenderer.material = hoverMaterial;
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        // Если объект захвачен, не сбрасываем цвет при выходе из hover
        if (!grabInteractable.isSelected)
            meshRenderer.material = defaultMaterial;
    }

    void OnSelectEnter(SelectEnterEventArgs args)
    {
        // При захвате всегда ставим select-цвет
        meshRenderer.material = selectMaterial;
    }

    void OnSelectExit(SelectExitEventArgs args)
    {
        // При отпускании возвращаемся к hover или default
        // Если луч всё ещё на объекте — hover, иначе default
        if (grabInteractable.isHovered)
            meshRenderer.material = hoverMaterial;
        else
            meshRenderer.material = defaultMaterial;
    }
}
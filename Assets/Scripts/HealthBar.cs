using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float timetoDrain = 0.5f;
    [SerializeField] private Gradient healthBarGradient;

    private Image image;
    private float target = 1f;
    private Color newHealthBarColor;
    private Coroutine drainHealthBarCoroutine;

    private void Start()
    {
        image = GetComponent<Image>();
        CheckHealthBarGradientAmount();
    }

    private void LateUpdate()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }

    public void UpdateHealthBar(float maxHealth , float currentHealth)
    {
        target = currentHealth / maxHealth;
        drainHealthBarCoroutine = StartCoroutine(DrainHealthBar());
        image.color = healthBarGradient.Evaluate(target);
        CheckHealthBarGradientAmount();
    }

    private IEnumerator DrainHealthBar()
    {
        float fillAmount = image.fillAmount;
        Color currentColor = image.color;

        float elapsedTime = 0f;
        while (elapsedTime < timetoDrain)
        {

            elapsedTime += Time.deltaTime;

            image.fillAmount = Mathf.Lerp(fillAmount , target , (elapsedTime / timetoDrain));
            image.color = Color.Lerp(currentColor, newHealthBarColor, (elapsedTime / timetoDrain));

            yield return null;
        }
    }

    private void CheckHealthBarGradientAmount()
    {
        newHealthBarColor = healthBarGradient.Evaluate(target);
    }
}

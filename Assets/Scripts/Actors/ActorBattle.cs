using Assets.Scripts.Items;
using Assets.Scripts.Services;
using UnityEngine;

public class ActorBattle : MonoBehaviour
{
    [SerializeReference, SubclassSelector]
    public IInputService inputService;

    [SerializeField]
    public ItemInstance primaryInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

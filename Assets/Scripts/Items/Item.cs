using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour {

    protected PlayerController player;
    private Text uiText;
    private bool pickUpAllowed;


    void Awake() {
        uiText = GameObject.Find("InteractText").GetComponent<Text>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        HideText();
    }

    void DisplayText() {
        uiText.text = "Press 'E' to pickup " + this.GetType().Name;
        uiText.enabled = true;
    }

    private void HideText() {
        uiText.enabled = false;
    }

    void Update() {

        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E)) {
            // we're picked up, so let's disable the ui text,
            HideText();

            // give the player a callback to inform the pickup,
            //player.PickUp(this);
            Interact();

            // and remove the object
            DeleteItem();
        }
    }

    protected abstract void Interact();

    void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.name == "Player") {
            DisplayText();
            pickUpAllowed = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            HideText();
            pickUpAllowed = false;
        }
    }

    protected void DeleteItem() {
        Destroy(gameObject);
    }

}
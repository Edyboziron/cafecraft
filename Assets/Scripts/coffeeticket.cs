using UnityEngine;
using TMPro; 

public class coffeeticket : MonoBehaviour
{
    public int coffeeTickets = 10;

    public TextMeshProUGUI ticketText; 

    void Start()
    {
        UpdateTicketText();
    }

    public void UpdateTicketText()
    {
        ticketText.text = "Tickets: " + coffeeTickets.ToString();
    }
}

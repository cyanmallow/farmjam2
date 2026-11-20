using System.Collections.Generic;
using UnityEngine;

public class OptionsFarmPlot : MonoBehaviour
{
    public static OptionsFarmPlot Instance { get; private set; }
    private IField currentField;
    private void Awake() => Instance = this;
    public void DoWhatWithPlot(IField field)
    {
        currentField = field;

        var options = new List<Option> {
            new Option { 
                optionName = "Plant", 
                action = ChosePlant, 
                optionTime = 1, 
                isOptionAvailable = currentField.CanPlant,
                unavailableReason = $"Can't plant, this plot is {currentField.StateName}."
            },
            new Option { 
                optionName = "Water", 
                action = () => currentField?.Water(), 
                optionTime = 1,
                isOptionAvailable = currentField.CanWater,
                unavailableReason = $"Can't water, this plot is {currentField.StateName} or already watered."
            },
            new Option {
                optionName = "Harvest",
                action = () => currentField?.Harvest(),
                optionTime = 1,
                isOptionAvailable = currentField.CanHarvest,
                unavailableReason = $"Can't harvest, this plot is {currentField.StateName}."
            },
            new Option {
                optionName = "Clear",
                action = () => currentField?.Clear(),
                optionTime = 1,
                isOptionAvailable = currentField.CanClear,
                unavailableReason = $"Can't clear, this plot is {currentField.StateName}."
            }
        };
        PopulateButtons.Instance.GiveOptions(options, chosen => chosen.action?.Invoke());
    }

    [SerializeField] private List<SeedData> allSeeds;
    private void ChosePlant()
    {
        var seeds = new List<Option>();

        foreach (var seed in allSeeds)
        {
            int owned = Inventory.Instance.GetItemCount(seed);
            var s = seed; // keep a copy for the lambda

            string reason = !currentField.CanPlant
                ? $"Can't plant, this plot is {currentField.StateName}."
                : $"You have no {s.itemName} seeds.";

            seeds.Add(new Option
            {
                optionName = $"{s.itemName} x{owned}",
                isOptionAvailable = currentField.CanPlant && owned > 0,
                unavailableReason = reason,
                action = () =>
                {
                    if (Inventory.Instance.RemoveItem(s, 1))
                        currentField?.Plant(s);
                }
            });
        }
        PopulateButtons.Instance.GiveOptions(seeds, chosen => chosen.action?.Invoke());
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata
{
    public class Checkout : ICheckout
    {
        private readonly Dictionary<string, float> _rules;
        private readonly Dictionary<string[], float> _specials;
        private List<string> _items;

        private List<string> _calculatedItems; 

        public Checkout(Dictionary<string, float> rules, Dictionary<string[], float> specials)
        {
            _rules = rules;
            _specials = specials;

            _items = new List<string>();
        }

        public void Scan(string item)
        {
            if (!string.IsNullOrEmpty(item) && _rules.ContainsKey(item))
                _items.Add(item);
        }

        public float Total()
        {
            _calculatedItems = _items.ToList();
            float result = ProcessSpecials(0);

            return ProcessItems(result);
        }

        private float ProcessItems(float total)
        {
            foreach (var item in _calculatedItems)
            {
                if (_rules.ContainsKey(item))
                    total += _rules[item];
            }
            return total;
        }

        private float ProcessSpecials(float total)
        {
            foreach (var special in _specials)
            {
                total = ProcessSpecial(special, total);
            }

            return total;
        }

        private float ProcessSpecial(KeyValuePair<string[], float> special, float total)
        {
            var specialItems = _calculatedItems.ToList();
            var hasSpecial = true;

            while (hasSpecial)
            {
                foreach (var specialRule in special.Key)
                {
                    if (specialItems.Contains(specialRule))
                    {
                        specialItems.Remove(specialRule);
                    }
                    else
                    {
                        hasSpecial = false;
                    }
                }

                if (hasSpecial)
                {
                    total += special.Value;
                    _calculatedItems = specialItems.ToList();
                }
            }

            return total;
        }
    }
}

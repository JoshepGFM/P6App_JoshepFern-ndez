using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using ShoppingP6_JoshepFernandez.Models;

namespace ShoppingP6_JoshepFernandez.ViewModels
{
    public class InventoryViewModel : BaseViewModel
    {
        public Inventory MyInventory { get; set; }

        public InventoryViewModel()
        {
            MyInventory = new Inventory();
        }

        public async Task<ObservableCollection<Inventory>> GetFullInventoriesList()
        {
            if (IsBusy)
            {
                return null;
            }
            else
            {
                IsBusy = true;

                try
                {
                    ObservableCollection<Inventory> list = new ObservableCollection<Inventory>();

                    list = await MyInventory.GetItemListFull();

                    if (list == null)
                    {
                        return null;
                    }

                    return list;
                }
                catch (Exception e)
                {
                    return null;

                }
                finally { IsBusy = false; }

            }

        }
    }
}

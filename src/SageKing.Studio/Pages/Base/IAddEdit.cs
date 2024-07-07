using Microsoft.AspNetCore.Components;

namespace SageKing.Studio.Pages.Base;

public interface IAddEdit<T> where
    T : EntityBase, new()
{
    public BaseService<T> DataService { get; }

    public string Title { get; set; }

    public T model { get; set; }

    public AntDesign.Form<T> form { get; set; }

    public async Task<bool> Save()
    {
        if (!form.Validate())
        {
            return false;
        };

        if (model.Id <= 0)
        {
            var getSave = await DataService.AddReturnEntityAsync(model);
            model.Id = getSave.Id;
        }
        else
        {
            await DataService.Update(model);
        }
        return true;
    }
    public void Reset()
    {
        model = default(T);
        form.Reset();
    }
}

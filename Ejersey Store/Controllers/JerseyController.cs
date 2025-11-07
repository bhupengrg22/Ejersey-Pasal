using jerseyShoppingCartMvcUI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace jerseyShoppingCartMvcUI.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]
public class jerseyController : Controller
{
    private readonly IjerseyRepository _jerseyRepo;
    private readonly IcategoryRepository _categoryRepo;
    private readonly IFileService _fileService;

    public jerseyController(IjerseyRepository jerseyRepo, IcategoryRepository categoryRepo, IFileService fileService)
    {
        _jerseyRepo = jerseyRepo;
        _categoryRepo = categoryRepo;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index()
    {
        var jerseys = await _jerseyRepo.Getjerseys();
        return View(jerseys);
    }

    public async Task<IActionResult> Addjersey()
    {
        var categorySelectList = (await _categoryRepo.Getcategorys()).Select(category => new SelectListItem
        {
            Text = category.categoryName,
            Value = category.Id.ToString(),
        });
        jerseyDTO jerseyToAdd = new() { categoryList = categorySelectList };
        return View(jerseyToAdd);
    }

    [HttpPost]
    public async Task<IActionResult> Addjersey(jerseyDTO jerseyToAdd)
    {
        var categorySelectList = (await _categoryRepo.Getcategorys()).Select(category => new SelectListItem
        {
            Text = category.categoryName,
            Value = category.Id.ToString(),
        });
        jerseyToAdd.categoryList = categorySelectList;

        if (!ModelState.IsValid)
            return View(jerseyToAdd);

        try
        {
            if (jerseyToAdd.ImageFile != null)
            {
                if(jerseyToAdd.ImageFile.Length> 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg",".jpg",".png"];
                string imageName=await _fileService.SaveFile(jerseyToAdd.ImageFile, allowedExtensions);
                jerseyToAdd.Image = imageName;
            }
            // manual mapping of jerseyDTO -> jersey
            jersey jersey = new()
            {
                Id = jerseyToAdd.Id,
                jerseyName = jerseyToAdd.jerseyName,
               
                Image = jerseyToAdd.Image,
                categoryId = jerseyToAdd.categoryId,
                Price = jerseyToAdd.Price
            };
            await _jerseyRepo.Addjersey(jersey);
            TempData["successMessage"] = "jersey is added successfully";
            return RedirectToAction(nameof(Addjersey));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"]= ex.Message;
            return View(jerseyToAdd);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(jerseyToAdd);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(jerseyToAdd);
        }
    }

    public async Task<IActionResult> Updatejersey(int id)
    {
        var jersey = await _jerseyRepo.GetjerseyById(id);
        if(jersey==null)
        {
            TempData["errorMessage"] = $"jersey with the id: {id} does not found";
            return RedirectToAction(nameof(Index));
        }
        var categorySelectList = (await _categoryRepo.Getcategorys()).Select(category => new SelectListItem
        {
            Text = category.categoryName,
            Value = category.Id.ToString(),
            Selected=category.Id==jersey.categoryId
        });
        jerseyDTO jerseyToUpdate = new() 
        { 
            categoryList = categorySelectList,
            jerseyName=jersey.jerseyName,
           
            categoryId=jersey.categoryId,
            Price=jersey.Price,
            Image=jersey.Image 
        };
        return View(jerseyToUpdate);
    }

    [HttpPost]
    public async Task<IActionResult> Updatejersey(jerseyDTO jerseyToUpdate)
    {
        var categorySelectList = (await _categoryRepo.Getcategorys()).Select(category => new SelectListItem
        {
            Text = category.categoryName,
            Value = category.Id.ToString(),
            Selected=category.Id==jerseyToUpdate.categoryId
        });
        jerseyToUpdate.categoryList = categorySelectList;

        if (!ModelState.IsValid)
            return View(jerseyToUpdate);

        try
        {
            string oldImage = "";
            if (jerseyToUpdate.ImageFile != null)
            {
                if (jerseyToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                string imageName = await _fileService.SaveFile(jerseyToUpdate.ImageFile, allowedExtensions);
                // hold the old image name. Because we will delete this image after updating the new
                oldImage = jerseyToUpdate.Image;
                jerseyToUpdate.Image = imageName;
            }
            // manual mapping of jerseyDTO -> jersey
            jersey jersey = new()
            {
                Id=jerseyToUpdate.Id,
                jerseyName = jerseyToUpdate.jerseyName,
             
                categoryId = jerseyToUpdate.categoryId,
                Price = jerseyToUpdate.Price,
                Image = jerseyToUpdate.Image
            };
            await _jerseyRepo.Updatejersey(jersey);
            // if image is updated, then delete it from the folder too
            if(!string.IsNullOrWhiteSpace(oldImage))
            {
                _fileService.DeleteFile(oldImage);
            }
            TempData["successMessage"] = "jersey is updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(jerseyToUpdate);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(jerseyToUpdate);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(jerseyToUpdate);
        }
    }

    public async Task<IActionResult> Deletejersey(int id)
    {
        try
        {
            var jersey = await _jerseyRepo.GetjerseyById(id);
            if (jersey == null)
            {
                TempData["errorMessage"] = $"jersey with the id: {id} does not found";
            }
            else
            {
                await _jerseyRepo.Deletejersey(jersey);
                if (!string.IsNullOrWhiteSpace(jersey.Image))
                {
                    _fileService.DeleteFile(jersey.Image);
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on deleting the data";
        }
        return RedirectToAction(nameof(Index));
    }

}

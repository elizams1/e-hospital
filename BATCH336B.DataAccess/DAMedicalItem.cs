using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;

namespace BATCH336B.DataAccess
{
    public class DAMedicalItem
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336BContext db;
        public DAMedicalItem(BATCH336BContext _db)
        {
            db = _db;
        }

        public VMResponse GetAll() => GetByFilter("");

        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMMMedicalItem> data = (
                    from mi in db.MMedicalItems
                    join mic in db.MMedicalItemCategories on mi.MedicalItemCategoryId equals mic.Id
                    join mis in db.MMedicalItemSegmentations on mi.MedicalItemSegmentationId equals mis.Id
                    where mi.IsDelete == false
                    && (mi.Name.Contains(filter ?? "") || mi.Indication.Contains(filter ?? "") || mic.Name.Contains(filter ?? "") || mis.Name.Contains(filter ?? ""))
                    select new VMMMedicalItem
                    {
                        Id = mi.Id,
                        Name = mi.Name,
                        MedicalItemCategoryId = mi.MedicalItemCategoryId,
                        MedicalItemCategoryName = mic.Name,

                        Composition = mi.Composition,

                        MedicalItemSegmentationId = mi.MedicalItemSegmentationId,
                        MedicalItemSegmentationName = mis.Name,

                        Manufacturer = mi.Manufacturer,
                        Indication = mi.Indication,
                        Dosage = mi.Dosage,
                        Directions = mi.Directions,
                        Contraindication = mi.Contraindication,
                        Caution = mi.Caution,
                        Packaging = mi.Packaging,
                        PriceMax = mi.PriceMax,
                        PriceMin = mi.PriceMin,
                        Image = mi.Image,
                        ImagePath = mi.ImagePath,


                        CreatedBy = mi.CreatedBy,
                        CreatedOn = mi.CreatedOn,
                        ModifiedBy = mi.ModifiedBy,
                        ModifiedOn = mi.ModifiedOn,
                        DeletedBy = mi.DeletedBy,
                        DeletedOn = mi.DeletedOn,
                        IsDelete = mi.IsDelete
                    }
                    ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                        ? $"{data.Count} Medical item data Successfuly fetched!"
                        : "Medical item has no Data!";
                response.StatusCode = (data.Count > 0)
                    ? System.Net.HttpStatusCode.OK
                    : System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }
        
        public VMResponse GetByFilter(string segmen, string filter, int? minPrice, int? maxPrice, string cat)
        {
            try
            {
                List<VMMMedicalItem> data = (
                    from mi in db.MMedicalItems
                    join mic in db.MMedicalItemCategories on mi.MedicalItemCategoryId equals mic.Id
                    join mis in db.MMedicalItemSegmentations on mi.MedicalItemSegmentationId equals mis.Id
                    where mi.IsDelete == false
                        && mis.Name.Contains(segmen ?? "")
                        && (mi.Name.Contains(filter ?? "") || mi.Indication.Contains(filter ?? ""))
                        && (mi.PriceMin >= (minPrice ?? 0) || mi.PriceMax <= (maxPrice ?? 0))
                        && mic.Name.Contains(cat ?? "")
                    select new VMMMedicalItem
                    {
                        Id = mi.Id,
                        Name = mi.Name,
                        MedicalItemCategoryId = mi.MedicalItemCategoryId,
                        MedicalItemCategoryName = mic.Name,

                        Composition = mi.Composition,

                        MedicalItemSegmentationId = mi.MedicalItemSegmentationId,
                        MedicalItemSegmentationName = mis.Name,

                        Manufacturer = mi.Manufacturer,
                        Indication = mi.Indication,
                        Dosage = mi.Dosage,
                        Directions = mi.Directions,
                        Contraindication = mi.Contraindication,
                        Caution = mi.Caution,
                        Packaging = mi.Packaging,
                        PriceMin = mi.PriceMin,
                        PriceMax = mi.PriceMax,
                        Image = mi.Image,
                        ImagePath = mi.ImagePath,


                        CreatedBy = mi.CreatedBy,
                        CreatedOn = mi.CreatedOn,
                        ModifiedBy = mi.ModifiedBy,
                        ModifiedOn = mi.ModifiedOn,
                        DeletedBy = mi.DeletedBy,
                        DeletedOn = mi.DeletedOn,
                        IsDelete = mi.IsDelete
                    }
                    ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                        ? $"{data.Count} Medical item data Successfuly fetched!"
                        : "Medical item has no Data!";
                response.StatusCode = (data.Count > 0)
                    ? System.Net.HttpStatusCode.OK
                    : System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse GetById(long id)
        {
            try
            {
                if (id > 0)
                {
                    VMMMedicalItem? data = (
                    from mi in db.MMedicalItems
                    join mic in db.MMedicalItemCategories on mi.MedicalItemCategoryId equals mic.Id
                    join mis in db.MMedicalItemSegmentations on mi.MedicalItemSegmentationId equals mis.Id
                    where mi.IsDelete == false
                        && mi.Id == id
                    select new VMMMedicalItem
                    {
                        Id = mi.Id,
                        Name = mi.Name,
                        MedicalItemCategoryId = mi.MedicalItemCategoryId,
                        MedicalItemCategoryName = mic.Name,

                        Composition = mi.Composition,

                        MedicalItemSegmentationId = mi.MedicalItemSegmentationId,
                        MedicalItemSegmentationName = mis.Name,

                        Manufacturer = mi.Manufacturer,
                        Indication = mi.Indication,
                        Dosage = mi.Dosage,
                        Directions = mi.Directions,
                        Contraindication = mi.Contraindication,
                        Caution = mi.Caution,
                        Packaging = mi.Packaging,
                        PriceMax = mi.PriceMax,
                        PriceMin = mi.PriceMin,
                        Image = mi.Image,
                        ImagePath = mi.ImagePath,


                        CreatedBy = mi.CreatedBy,
                        CreatedOn = mi.CreatedOn,
                        ModifiedBy = mi.ModifiedBy,
                        ModifiedOn = mi.ModifiedOn,
                        DeletedBy = mi.DeletedBy,
                        DeletedOn = mi.DeletedOn,
                        IsDelete = mi.IsDelete
                    }
                    ).FirstOrDefault();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = "Medical Item data Successfuly fetched!";
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Medical Item data could not be found";
                        response.StatusCode = System.Net.HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = "Please input the corred ID";
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse Create(VMMMedicalItem data)
        {
            //starting db trasaction proses
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    //preapare empty data model for new category data
                    MMedicalItem medItem = new MMedicalItem();

                    medItem.Name = data.Name;

                    medItem.MedicalItemCategoryId = data.MedicalItemCategoryId;
                    
                    medItem.Composition = data.Composition;
                    
                    medItem.MedicalItemSegmentationId = data.MedicalItemSegmentationId;
                    
                    medItem.Manufacturer = data.Manufacturer;
                    medItem.Indication = data.Indication;
                    medItem.Dosage = data.Dosage;
                    medItem.Directions = data.Directions;
                    medItem.Contraindication = data.Contraindication;
                    medItem.Caution = data.Caution;
                    medItem.Packaging = data.Packaging;
                    
                    medItem.PriceMax = data.PriceMax;
                    medItem.PriceMin = data.PriceMin;
                    
                    medItem.Image = data.Image;
                    medItem.ImagePath = data.ImagePath;


                    medItem.IsDelete = false;
                    medItem.CreatedBy = data.CreatedBy;
                    medItem.CreatedOn = DateTime.Now;

                    //proses membuat data kedalam DB tabel
                    db.Add(medItem);
                    db.SaveChanges();

                    //save table changes from database transaction
                    dbTran.Commit();

                    //update API Respon
                    response.data = medItem;
                    response.message = "New Medical Item has been succsesfully created";
                    response.StatusCode = System.Net.HttpStatusCode.Created;
                }
                catch (Exception ex)
                {
                    //undo table changes from database transaction proses
                    dbTran.Rollback();

                    response.data = data;
                    response.message = ex.Message;
                }
            };
            return response;
        }

        public VMResponse Update(VMMMedicalItem data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMMedicalItem? existingData = (VMMMedicalItem?)GetById(data.Id).data;

                    if (existingData != null)
                    {
                        MMedicalItem medItem = new MMedicalItem()
                        {
                            Id = existingData.Id,
                            
                            Name = data.Name,
                            MedicalItemCategoryId = data.MedicalItemCategoryId,
                            Composition = data.Composition,
                            MedicalItemSegmentationId = data.MedicalItemSegmentationId,
                            Manufacturer = data.Manufacturer,
                            Indication = data.Indication,
                            Dosage = data.Dosage,
                            Directions = data.Contraindication,
                            Caution = data.Caution,
                            Packaging = data.Packaging,
                            PriceMax = data.PriceMax,
                            PriceMin = data.PriceMin,
                            Image = data.Image,
                            ImagePath = data.ImagePath,

                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,

                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,

                            IsDelete = false,
                        };
                        //proses membuat data kedalam BD tabel
                        db.Update(medItem);
                        db.SaveChanges();

                        //save table changes from database transaction
                        dbTran.Commit();                //biar masuk ke database

                        //update API Respon
                        response.data = medItem;
                        response.message = $"Medical Item with name={data.Name} has been succsesfully update";
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.data = data;
                        response.message = $"Request Medical Item data cannot be Updated";
                        response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    }

                }
                catch (Exception ex)
                {
                    //undo table changes from database transaction proses
                    dbTran.Rollback();

                    response.data = data;
                    response.message = ex.Message;
                }
            }
            return response;
        }

        public VMResponse Delete(int Id, int userId)
        {
            if (Id != 0 && userId != 0)
            {
                using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        VMMMedicalItem? existingData = (VMMMedicalItem?)GetById(Id).data;
                        if (existingData != null)
                        {
                            MMedicalItem medItem = new MMedicalItem()
                            {
                                Id = existingData.Id,
                                Name = existingData.Name,
                                MedicalItemCategoryId = existingData.MedicalItemCategoryId,
                                Composition = existingData.Composition,
                                MedicalItemSegmentationId = existingData.MedicalItemSegmentationId,
                                Manufacturer = existingData.Manufacturer,
                                Indication = existingData.Indication,
                                Dosage = existingData.Dosage,
                                Directions = existingData.Contraindication,
                                Caution = existingData.Caution,
                                Packaging = existingData.Packaging,
                                PriceMax = existingData.PriceMax,
                                PriceMin = existingData.PriceMin,
                                Image = existingData.Image,
                                ImagePath = existingData.ImagePath,

                                CreatedBy = existingData.CreatedBy,
                                CreatedOn = existingData.CreatedOn,

                                ModifiedBy = userId,
                                ModifiedOn = DateTime.Now,

                                DeletedBy = userId,
                                DeletedOn = DateTime.Now,

                                IsDelete = true,
                            };
                            //proses membuat data kedalam BD tabel
                            db.Update(medItem);
                            db.SaveChanges();

                            //save table changes from database transaction
                            dbTran.Commit();                //biar masuk ke database

                            //update API Respon
                            response.data = medItem;
                            response.message = $"Medical Item with name={existingData.Name} has benn succsesfully Deleted";
                            response.StatusCode = System.Net.HttpStatusCode.OK;

                        }
                        else
                        {
                            response.message = $"Medical Item with ID{Id} cannot be found";
                            response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        response.message = ex.Message;
                    }
                }
            }
            else
            {
                response.message = "Please input Medical Item ID and User ID first";
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            return response;
        }

    }
}

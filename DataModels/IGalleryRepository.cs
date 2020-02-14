using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.Models
{
    interface IGalleryRepository
    {
        Gallery AddGallery(Gallery gallery);
        Gallery UpdateGallery(Gallery galleryChanges);
        Gallery DeleteGallery(int id);
        Gallery fetchGallery(int id);
        IEnumerable<Gallery> fetchAllGallery();
    }
}

using $ext_safeprojectname$.SharedKernel.Interfaces;
using MongoDB.Bson;
using System;

namespace $ext_safeprojectname$.SharedKernel
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}

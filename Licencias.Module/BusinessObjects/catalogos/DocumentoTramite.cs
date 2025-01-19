using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Licencias.Module.BusinessObjects.catalogos
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [NavigationItem("Catálogos")]
    [DefaultProperty("NombreDocumento")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DocumentoTramite : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public DocumentoTramite(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private string nombreDocumento;
        [Size(255)]
        public string NombreDocumento
        {
            get => nombreDocumento;
            set => SetPropertyValue(nameof(NombreDocumento), ref nombreDocumento, value);
        }

        private string rutaArchivo;
        [Size(1024)]
        public string RutaArchivo
        {
            get => rutaArchivo;
            set => SetPropertyValue(nameof(RutaArchivo), ref rutaArchivo, value);
        }

        private ModoPresentacion modoPresentacion;
        public ModoPresentacion ModoPresentacion
        {
            get => modoPresentacion;
            set => SetPropertyValue(nameof(ModoPresentacion), ref modoPresentacion, value);
        }

        // Relación con el trámite
        private TramiteLicencia tramite;
        [Association("TramiteLicencia-Documentos")]
        public TramiteLicencia Tramite
        {
            get => tramite;
            set => SetPropertyValue(nameof(Tramite), ref tramite, value);
        }
    }

    public enum ModoPresentacion
    {
        EnLinea = 0,
        Presencial = 1
    }
}
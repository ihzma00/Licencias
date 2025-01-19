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

namespace Licencias.Module.BusinessObjects.citas
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [NavigationItem("Cita")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DocumentoCita : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public DocumentoCita(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        // --------------------------------------------------------------------
        // RELACIÓN CON CITA
        // --------------------------------------------------------------------
        private Cita cita;
        [Association("Cita-DocumentosCita")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Cita Cita
        {
            get => cita;
            set => SetPropertyValue(nameof(Cita), ref cita, value);
        }

        // --------------------------------------------------------------------
        // DATOS BÁSICOS DEL DOCUMENTO
        // --------------------------------------------------------------------
        private string nombreDocumento;
        [Size(255)]
        [RuleRequiredField(DefaultContexts.Save)]
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

        private EstatusDocumentoCita estatusDocumento;
        public EstatusDocumentoCita EstatusDocumento
        {
            get => estatusDocumento;
            set => SetPropertyValue(nameof(EstatusDocumento), ref estatusDocumento, value);
        }

        // --------------------------------------------------------------------
        // OBSERVACIONES / HISTORIAL DE CAMBIOS
        // --------------------------------------------------------------------
        [Association("DocumentoCita-Observaciones")]
        public XPCollection<ObservacionDocumentoCita> Observaciones
        {
            get { return GetCollection<ObservacionDocumentoCita>(nameof(Observaciones)); }
        }
    }

    public enum EstatusDocumentoCita
    {
        EnRevision = 0,
        Aprobado = 1,
        Rechazado = 2
    }
}
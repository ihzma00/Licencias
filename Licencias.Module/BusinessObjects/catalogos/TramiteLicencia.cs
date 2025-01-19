using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Licencias.Module.BusinessObjects.licencias;
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
    [Persistent("TramiteLicencia")]
    [DefaultProperty("TipoTramite")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class TramiteLicencia : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public TramiteLicencia(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        // --- TIPO DE TR�MITE ---
        private TipoTramite tipoTramite;
        [RuleRequiredField(DefaultContexts.Save)]
        public TipoTramite TipoTramite
        {
            get => tipoTramite;
            set => SetPropertyValue(nameof(TipoTramite), ref tipoTramite, value);
        }

        // --- PERSONA QUE REALIZA EL TR�MITE ---
        private Ciudadano persona;
        [Association("Ciudadano-TramitesLicencia")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Ciudadano Persona
        {
            get => persona;
            set => SetPropertyValue(nameof(Persona), ref persona, value);
        }

        // --- LICENCIA (para renovaci�n, suspensi�n, etc.) ---
        private Licencia licencia;
        public Licencia Licencia
        {
            get => licencia;
            set => SetPropertyValue(nameof(Licencia), ref licencia, value);
        }

        // --- ESTATUS DEL TR�MITE ---
        private TromiteEstatus estatusTramite;
        public TromiteEstatus EstatusTramite
        {
            get => estatusTramite;
            set => SetPropertyValue(nameof(EstatusTramite), ref estatusTramite, value);
        }

        // --- FECHAS ---
        private DateTime fechaSolicitud;
        public DateTime FechaSolicitud
        {
            get => fechaSolicitud;
            set => SetPropertyValue(nameof(FechaSolicitud), ref fechaSolicitud, value);
        }

        private DateTime? fechaConclusion;
        public DateTime? FechaConclusion
        {
            get => fechaConclusion;
            set => SetPropertyValue(nameof(FechaConclusion), ref fechaConclusion, value);
        }

        // --- PAGO VALIDADO ---
        private bool pagoValidado;
        public bool PagoValidado
        {
            get => pagoValidado;
            set => SetPropertyValue(nameof(PagoValidado), ref pagoValidado, value);
        }

        // --- DOCUMENTOS DEL TR�MITE ---
        [Association("TramiteLicencia-Documentos")]
        public XPCollection<DocumentoTramite> Documentos
        {
            get { return GetCollection<DocumentoTramite>(nameof(Documentos)); }
        }
    }

    // Se podr�a usar una tabla en lugar de enum, pero como ejemplo:
    public enum TromiteEstatus
    {
        EnProceso = 0,
        Completado = 1,
        Cancelado = 2
    }
}
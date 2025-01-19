using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
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

namespace Licencias.Module.BusinessObjects.citas
{
    [DefaultClassOptions]
    [NavigationItem("Cita")]
    [Persistent("Cita")]
    [Appearance("DisableFechaHoraIfNotApproved",
        AppearanceItemType = "ViewItem",
        TargetItems = "FechaHora",
        Criteria = "Documentos[ EstatusDocumento != ##Enum#MiProyecto.Module.BusinessObjects.EstatusDocumentoCita,Aprobado# ].Count() > 0",
        Enabled = false)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Cita : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Cita(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        // --------------------------------------------------------------------
        // RELACIÓN CON CIUDADANO
        // --------------------------------------------------------------------
        private Ciudadano ciudadano;
        [Association("Ciudadano-Citas")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Ciudadano Ciudadano
        {
            get => ciudadano;
            set => SetPropertyValue(nameof(Ciudadano), ref ciudadano, value);
        }

        // --------------------------------------------------------------------
        // DATOS BÁSICOS DE LA CITA
        // --------------------------------------------------------------------
        private DateTime fechaHora;
        public DateTime FechaHora
        {
            get => fechaHora;
            set => SetPropertyValue(nameof(FechaHora), ref fechaHora, value);
        }

        private int duracionMinutos;
        public int DuracionMinutos
        {
            get => duracionMinutos;
            set => SetPropertyValue(nameof(DuracionMinutos), ref duracionMinutos, value);
        }

        private EstatusCita estatus;
        public EstatusCita Estatus
        {
            get => estatus;
            set => SetPropertyValue(nameof(Estatus), ref estatus, value);
        }

        // --------------------------------------------------------------------
        // DOCUMENTOS SUBIDOS PARA ESTA CITA
        // --------------------------------------------------------------------
        [Association("Cita-DocumentosCita")]
        public XPCollection<DocumentoCita> Documentos
        {
            get { return GetCollection<DocumentoCita>(nameof(Documentos)); }
        }
    }

    public enum EstatusCita
    {
        Programada = 0,
        Atendida = 1,
        Cancelada = 2
    }

    //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
    //public void ActionMethod() {
    //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
    //    this.PersistentProperty = "Paid";
    //}

}
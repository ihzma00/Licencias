using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Licencias.Module.BusinessObjects.catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Licencias.Module.BusinessObjects.licencias
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [NavigationItem("Licencia")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Licencia : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Licencia(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        // --- NUMERO DE LICENCIA ---
        private string numeroLicencia;
        [Size(50)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string NumeroLicencia
        {
            get => numeroLicencia;
            set => SetPropertyValue(nameof(NumeroLicencia), ref numeroLicencia, value);
        }

        // --- PERSONA (dueña de la licencia) ---
        private Ciudadano persona;
        [Association("Ciudadano-Licencias")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Ciudadano Persona
        {
            get => persona;
            set => SetPropertyValue(nameof(Persona), ref persona, value);
        }

        // --- TIPO DE LICENCIA ---
        private TipoLicencia tipoLicencia;
        [RuleRequiredField(DefaultContexts.Save)]
        public TipoLicencia TipoLicencia
        {
            get => tipoLicencia;
            set => SetPropertyValue(nameof(TipoLicencia), ref tipoLicencia, value);
        }

        // --- ESTATUS DE LA LICENCIA ---
        private EstatusLicencia estatusLicencia;
        [RuleRequiredField(DefaultContexts.Save)]
        public EstatusLicencia EstatusLicencia
        {
            get => estatusLicencia;
            set => SetPropertyValue(nameof(EstatusLicencia), ref estatusLicencia, value);
        }

        // --- FECHAS ---
        private DateTime fechaEmision;
        public DateTime FechaEmision
        {
            get => fechaEmision;
            set => SetPropertyValue(nameof(FechaEmision), ref fechaEmision, value);
        }

        private DateTime fechaExpiracion;
        public DateTime FechaExpiracion
        {
            get => fechaExpiracion;
            set => SetPropertyValue(nameof(FechaExpiracion), ref fechaExpiracion, value);
        }

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}
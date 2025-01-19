using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Licencias.Module.BusinessObjects.catalogos;
using Licencias.Module.BusinessObjects.citas;
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
    public class Ciudadano : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Ciudadano(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        // --------------------------------------------------------------------
        // PROPIEDADES BÁSICAS
        // --------------------------------------------------------------------

        private string nombre;
        [Size(255)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Nombre
        {
            get => nombre;
            set => SetPropertyValue(nameof(Nombre), ref nombre, value);
        }

        private string primerApellido;
        [Size(255)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string PrimerApellido
        {
            get => primerApellido;
            set => SetPropertyValue(nameof(PrimerApellido), ref primerApellido, value);
        }

        private string segundoApellido;
        [Size(255)]
        public string SegundoApellido
        {
            get => segundoApellido;
            set => SetPropertyValue(nameof(SegundoApellido), ref segundoApellido, value);
        }

        private string domicilio;
        [Size(SizeAttribute.Unlimited)]
        public string Domicilio
        {
            get => domicilio;
            set => SetPropertyValue(nameof(Domicilio), ref domicilio, value);
        }

        private string correo;
        [Size(255)]
        [RuleRegularExpression(DefaultContexts.Save,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            CustomMessageTemplate = "Ingrese un correo electrónico válido.")]
        public string Correo
        {
            get => correo;
            set => SetPropertyValue(nameof(Correo), ref correo, value);
        }

        private string telefono;
        [Size(20)]
        public string Telefono
        {
            get => telefono;
            set => SetPropertyValue(nameof(Telefono), ref telefono, value);
        }

        private string curp;
        [Size(18)] // Longitud típica de la CURP
        public string CURP
        {
            get => curp;
            set => SetPropertyValue(nameof(CURP), ref curp, value);
        }

        private string rfc;
        [Size(13)] // Longitud típica de RFC para persona física
        public string RFC
        {
            get => rfc;
            set => SetPropertyValue(nameof(RFC), ref rfc, value);
        }

        // --------------------------------------------------------------------
        // RELACIONES
        // --------------------------------------------------------------------

        // Relación con Licencia: un ciudadano puede tener varias licencias
        // Se asume que en Licencia.cs hay [Association("Ciudadano-Licencias")] 
        [Association("Ciudadano-Licencias")]
        public XPCollection<Licencia> Licencias
        {
            get { return GetCollection<Licencia>(nameof(Licencias)); }
        }

        // Relación con TramiteLicencia: un ciudadano puede realizar varios trámites
        // Se asume que en TramiteLicencia.cs hay [Association("Ciudadano-TramitesLicencia")]
        [Association("Ciudadano-TramitesLicencia")]
        public XPCollection<TramiteLicencia> TramitesLicencia
        {
            get { return GetCollection<TramiteLicencia>(nameof(TramitesLicencia)); }
        }


        [Association("Ciudadano-Citas")]
        public XPCollection<Cita> Citas
        {
            get { return GetCollection<Cita>(nameof(Citas)); }
        }

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}
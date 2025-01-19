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
    [Persistent("TipoTramite")]
    [DefaultProperty("Descripcion")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class TipoTramite : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public TipoTramite(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        // --- CLAVE (m�ximo 2 caracteres, ejemplo: "M1", "A2", etc.) ---
        private string clave;

        // Usamos [Size(2)] para indicar longitud m�xima en la BD
        // y [RuleRequiredField] para requerirlo al guardar.
        // Opcionalmente, se puede agregar [RuleRegularExpression] 
        // si deseas restringir a alfanum�ricos u otro patr�n espec�fico.
        [Size(2)]
        [RuleRequiredField(DefaultContexts.Save,
            CustomMessageTemplate = "La clave es obligatoria y debe tener m�ximo 2 caracteres.")]
        [RuleRegularExpression(
        DefaultContexts.Save,
        @"^[A-Za-z0-9]{1,2}$",
        CustomMessageTemplate = "La clave debe tener 1 o 2 caracteres alfanum�ricos.")]
        public string Clave
        {
            get => clave;
            set => SetPropertyValue(nameof(Clave), ref clave, value);
        }


        private string descripcion;
        [Size(255)]
        public string Descripcion
        {
            get => descripcion;
            set => SetPropertyValue(nameof(Descripcion), ref descripcion, value);
        }

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}
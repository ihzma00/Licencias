using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
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
    public class ObservacionDocumentoCita : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public ObservacionDocumentoCita(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        // --------------------------------------------------------------------
        // RELACIÓN CON DOCUMENTO DE CITA
        // --------------------------------------------------------------------
        private DocumentoCita documentoCita;
        [Association("DocumentoCita-Observaciones")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DocumentoCita DocumentoCita
        {
            get => documentoCita;
            set => SetPropertyValue(nameof(DocumentoCita), ref documentoCita, value);
        }

        // --------------------------------------------------------------------
        // DETALLES DE LA OBSERVACIÓN
        // --------------------------------------------------------------------
        private DateTime fechaObservacion;
        public DateTime FechaObservacion
        {
            get => fechaObservacion;
            set => SetPropertyValue(nameof(FechaObservacion), ref fechaObservacion, value);
        }

        // Quién hace la observación (puede ser el revisor, un funcionario, etc.)
        // Si manejas usuarios de XAF, podrías referenciar la clase SecuritySystemUser.
        private string usuario;
        [Size(100)]
        public string Usuario
        {
            get => usuario;
            set => SetPropertyValue(nameof(Usuario), ref usuario, value);
        }

        private string comentario;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Comentario
        {
            get => comentario;
            set => SetPropertyValue(nameof(Comentario), ref comentario, value);
        }

        // Campo opcional para indicar la acción a tomar, por ejemplo "Corregir nombre", "Subir nuevamente", etc.
        private string sugerencia;
        [Size(SizeAttribute.Unlimited)]
        public string Sugerencia
        {
            get => sugerencia;
            set => SetPropertyValue(nameof(Sugerencia), ref sugerencia, value);
        }

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}
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
    [DefaultProperty("ClaveCompuesta")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class RequerimientoTipoLicenciaTramite : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public RequerimientoTipoLicenciaTramite(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        // --------------------------------------------------------------------
        // CAMPOS PARA LA CLAVE Y CONSECUTIVO
        // --------------------------------------------------------------------

        // 1) Campo consecutivo (se genera de manera incremental por combinaci�n)
        private int consecutivo;
        public int Consecutivo
        {
            get => consecutivo;
            set => SetPropertyValue(nameof(Consecutivo), ref consecutivo, value);
        }

        // 2) Campo clave compuesta (formada por tram->lic->req + consecutivo)
        private string claveCompuesta;
        [Size(50)]
        [RuleRequiredField(DefaultContexts.Save,
            CustomMessageTemplate = "No se ha podido generar la clave compuesta.")]
        public string ClaveCompuesta
        {
            get => claveCompuesta;
            set => SetPropertyValue(nameof(ClaveCompuesta), ref claveCompuesta, value);
        }

        // --------------------------------------------------------------------
        // OTROS CAMPOS O PROPIEDADES (EsObligatorio)
        // --------------------------------------------------------------------
        private bool esObligatorio;
        public bool EsObligatorio
        {
            get => esObligatorio;
            set => SetPropertyValue(nameof(EsObligatorio), ref esObligatorio, value);
        }

        // --------------------------------------------------------------------
        // RELACIONES
        // --------------------------------------------------------------------
        private TipoTramite tipoTramite;
        [RuleRequiredField(DefaultContexts.Save)]
        public TipoTramite TipoTramite
        {
            get => tipoTramite;
            set => SetPropertyValue(nameof(TipoTramite), ref tipoTramite, value);
        }

        private TipoLicencia tipoLicencia;
        [RuleRequiredField(DefaultContexts.Save)]
        public TipoLicencia TipoLicencia
        {
            get => tipoLicencia;
            set => SetPropertyValue(nameof(TipoLicencia), ref tipoLicencia, value);
        }

        private Requerimiento requerimiento;
        [Association("Requerimiento-RequerimientoTipoLicenciaTramites")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Requerimiento Requerimiento
        {
            get => requerimiento;
            set => SetPropertyValue(nameof(Requerimiento), ref requerimiento, value);
        }


        // --------------------------------------------------------------------
        // L�GICA PARA GENERAR CLAVE Y CONSECUTIVO
        // --------------------------------------------------------------------
        protected override void OnSaving()
        {
            base.OnSaving();

            // Si el objeto est� siendo eliminado, no hacemos nada:
            if (IsDeleted) return;

            // Validamos que tengamos los objetos relacionados
            if (TipoTramite == null || TipoLicencia == null || Requerimiento == null)
                return;

            // S�lo generamos el consecutivo si no se ha asignado antes (o si se desea regenerar).
            // Por ejemplo: if (Consecutivo == 0)
            if (Consecutivo <= 0)
            {
                // Tomamos todos los RequerimientoTipoLicenciaTramite con la MISMA combinaci�n
                var existentes = new XPCollection<RequerimientoTipoLicenciaTramite>(Session,
                    CriteriaOperator.Parse("TipoTramite = ? AND TipoLicencia = ? AND Requerimiento = ?",
                        TipoTramite, TipoLicencia, Requerimiento)
                );

                // Calculamos el mayor consecutivo existente + 1
                // (si no hay registros previos, se asigna 1)
                int nuevoConsecutivo = existentes.Any()
                    ? existentes.Max(x => x.Consecutivo) + 1
                    : 1;

                Consecutivo = nuevoConsecutivo;
            }

            // Generamos la clave compuesta. T� decides el formato. Ej. "E1-M1-R2-3"
            ClaveCompuesta = $"{TipoTramite.Clave}{TipoLicencia.Clave}{Requerimiento.Clave}{Consecutivo}";

            // Si prefieres separadores:
            // ClaveCompuesta = $"{TipoTramite.Clave}-{TipoLicencia.Clave}-{Requerimiento.Clave}-{Consecutivo}";
        }
    }
}
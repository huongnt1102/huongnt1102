
using System.Linq;

namespace BuildingDesignTemplate.Class
{
    public static class Form
    {
        private static readonly Library.MasterDataContext Db = new Library.MasterDataContext();

        public static Library.template_Form GetFormTemplateById(int? formId)
        {
            return Db.template_Forms.FirstOrDefault(_ => _.ReportId == formId);
        }

        public static string GetContentById(int? formId)
        {
            var form = GetFormTemplateById(formId);
            return form != null ? form.Content : "";
        }
    }
}

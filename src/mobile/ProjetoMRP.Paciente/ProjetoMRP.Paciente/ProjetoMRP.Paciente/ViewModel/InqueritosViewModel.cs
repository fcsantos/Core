using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMRP.Paciente.ViewModel
{
    public class InqueritosViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<QuestionViewModel> Questions { get; set; }
        public Guid InquiryScheduleId { get; set; }
    }

    public class QuestionViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Placeholder { get; set; }
        public bool? SingleLine { get; set; }
        public byte? SortOrder { get; set; }
        public string TypeOfAnswer { get; set; }
        public Guid InquiryId { get; set; }
        public string ValidationRule { get; set; }
        public Guid? CriterionGroupId { get; set; }
        public ICollection<AnswerOptionsViewModel> AnswerOptions { get; set; }

        public InqueritosViewModel Inquiry { get; set; }
        public CriterionGroupViewModel CriterionGroup { get; set; }
    }

    public class AnswerOptionsViewModel
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Option { get; set; }
        public byte? SortOrder { get; set; }
        public decimal AnswerValue { get; set; }
    }

    public class CriterionGroupViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CriteriaGroupType { get; set; }

        public IEnumerable<CriterionRuleViewModel> CriteriaRules { get; set; }
    }

    public class CriterionRuleViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public Guid CriterionGroupId { get; set; }
        public bool DisplayAlert { get; set; }
        public string AlertCssClass { get; set; }
    }
}

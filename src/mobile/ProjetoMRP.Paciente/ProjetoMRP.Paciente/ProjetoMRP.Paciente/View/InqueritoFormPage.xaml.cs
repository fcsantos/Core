using ProjetoMRP.Paciente.Services;
using ProjetoMRP.Paciente.Utilities.Load;
using ProjetoMRP.Paciente.ViewModel;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoMRP.Paciente.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InqueritoFormPage : ContentPage
    {
        public string userId;
        public Guid InquiryId;
        public string InquiryTitle;
        public Guid PatientId;
        public Guid InquiryScheduleId;
        public bool answered = false;
        public InquiryScheduleViewModel inqueritoModel;
        public InqueritoFormPage(InquiryScheduleViewModel inquerito)
        {
            InitializeComponent();
            InquiryId = inquerito.InquiryId;
            InquiryTitle = inquerito.InquiryTitle;
            PatientId = inquerito.PatientId;
            InquiryScheduleId = inquerito.Id;
            inqueritoModel = inquerito;
            Title = inquerito.InquiryTitle;
            CarregarInquerito(inquerito.InquiryId);
        }

        private async Task setAnsweredToTrue(Guid id)
        {
            if (answered)
            {
                inqueritoModel.answered = true;
                await ApiService.Put<InquiryScheduleViewModel>($"inquiries-schedule/{id}", inqueritoModel);

                await Application.Current.MainPage.DisplayAlert("Respondido", "Inquérito Enviado", "OK");
                await Navigation.PopToRootAsync();
                await Navigation.PopAllPopupAsync();
            }
            else
            {                
                await Navigation.PopAsync();
                await Navigation.PopAllPopupAsync();
            }
        }
        private async void CarregarInquerito(Guid id)
        {
            var inqueritoModel = await ApiService.GetBy<InqueritosViewModel>($"inquiries/{id}");

            foreach (var question in inqueritoModel.Questions)
            {
                switch (question.TypeOfAnswer)
                {
                    case "InputText":
                        stQuestions.Children.Add(new Label
                        {
                            Text = question.Title,
                            FontAttributes = FontAttributes.Bold,
                            FontSize = 18
                        });
                        if (question.SingleLine == true)
                        {
                            foreach (var answer in question.AnswerOptions)
                            {
                                stQuestions.Children.Add(new Entry
                                {
                                    Placeholder = question.Placeholder,
                                    AutomationId = question.Id.ToString() + "|" + question.Title,
                                    ClassId = answer.Id.ToString() + "|" + answer.AnswerValue,
                                    Margin = new Thickness(0, 0, 5, 10)
                                });
                            }
                        }
                        else
                        {
                            foreach (var answer in question.AnswerOptions)
                            {
                                double size = 100.0;
                                if (question.ValidationRule.Equals("Inteiro"))
                                {
                                    size = 50;
                                }

                                stQuestions.Children.Add(new Editor
                                {
                                    Placeholder = question.Placeholder,
                                    AutoSize = EditorAutoSizeOption.TextChanges,
                                    AutomationId = question.Id.ToString() + "|" + question.Title,
                                    ClassId = answer.Id.ToString() + "|" + answer.AnswerValue,
                                    Margin = new Thickness(0, 0, 5, 10),
                                    WidthRequest = size
                                });
                            }
                        }
                        break;
                    case "RadioButton":
                        stQuestions.Children.Add(new Label
                        {
                            Text = question.Title,
                            FontAttributes = FontAttributes.Bold,
                            FontSize = 18
                        });
                        foreach (var answer in question.AnswerOptions)
                        {
                            var rb = new RadioButton
                            {
                                GroupName = question.Id.ToString(),
                                Content = answer.Option,
                                AutomationId = question.Id.ToString() + "|" + question.Title,
                                ClassId = answer.Id.ToString() + "|" + answer.AnswerValue
                            };
                            stQuestions.Children.Add(rb);
                        }
                        break;
                    case "Checkboxes":
                        stQuestions.Children.Add(new Label
                        {
                            Text = question.Title,
                            FontAttributes = FontAttributes.Bold,
                            FontSize = 18
                        });
                        var grid = new Grid
                        {
                            RowDefinitions = {
                                                new RowDefinition { Height = GridLength.Auto },
                            },
                            ColumnDefinitions = {
                                                new ColumnDefinition { Width = new GridLength(30, GridUnitType.Absolute) },
                                                new ColumnDefinition {Width = new GridLength(.2, GridUnitType.Star) },
                            }
                        };
                        stQuestions.Children.Add(grid);
                        var linha = 0;
                        foreach (var answer in question.AnswerOptions)
                        {
                            var rb = new CheckBox
                            {
                                AutomationId = question.Id.ToString() + "|" + question.Title,
                                ClassId = answer.Id.ToString() + "|" + answer.AnswerValue + "|" + answer.Option
                            };

                            var lb = new Label
                            {
                                Text = answer.Option
                            };

                            grid.Children.Add(rb, 0, linha);
                            grid.Children.Add(lb, 1, linha);

                            linha += 1;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new Loading());

            var ListOfAnswers = new List<PatientAnswersViewModel>();
            var stControls = stQuestions.Children;

            foreach (var control in stControls)
            {
                if (control is Entry)
                {
                    PatientAnswersViewModel respostas = new PatientAnswersViewModel();
                    var entry = ((Entry)control);
                    if (entry.Text != null)
                    {
                        var question = entry.AutomationId.Split('|');

                        respostas.QuestionId = Guid.Parse(question[0]);
                        respostas.QuestionTitle = question[1];

                        var answer = entry.ClassId.Split('|');

                        respostas.AnswerOptionId = Guid.Parse(answer[0]);
                        respostas.AnswerText = entry.Text;
                        respostas.AnswerValue = Convert.ToDecimal(answer[1]);

                        ListOfAnswers.Add(respostas);
                    }


                }

                if (control is Editor)
                {
                    PatientAnswersViewModel respostas = new PatientAnswersViewModel();
                    var editor = ((Editor)control);

                    if (editor.Text != null)
                    {
                        var question = editor.AutomationId.Split('|');

                        respostas.QuestionId = Guid.Parse(question[0]);
                        respostas.QuestionTitle = question[1];

                        var answer = editor.ClassId.Split('|');

                        respostas.AnswerOptionId = Guid.Parse(answer[0]);
                        respostas.AnswerText = editor.Text;
                        respostas.AnswerValue = Convert.ToDecimal(answer[1]);

                        var outInt = 0;
                        var result = Int32.TryParse(editor.Text, out outInt);

                        if (result)
                        {
                            var questionViewModel = await ApiService.GetBy<QuestionViewModel>($"questions/{respostas.QuestionId}");
                            var questions = await ApiService.GetList<QuestionViewModel>($"questions/get-all-by-inquiryid/{InquiryId}");
                            var resultQuestions = questions.Where(x => x.CriterionGroup != null && x.CriterionGroupId == questionViewModel.CriterionGroupId);

                            foreach (var rq in resultQuestions.Select(x => x.CriterionGroup))
                            {
                                foreach (var cr in rq.CriteriaRules)
                                {
                                    if (Convert.ToInt32(editor.Text) >= cr.Minimum && Convert.ToInt32(editor.Text) <= cr.Maximum)
                                    {
                                        respostas.DisplayAlert = cr.DisplayAlert;
                                        respostas.AlertCssClass = cr.AlertCssClass;
                                        respostas.CriteriaGroupId = cr.CriterionGroupId;
                                        respostas.CriteriaGroupName = cr.Title;
                                    }
                                }
                            }
                        }

                        ListOfAnswers.Add(respostas);
                    }

                }

                if (control is RadioButton)
                {
                    PatientAnswersViewModel respostas = new PatientAnswersViewModel();
                    var radio = ((RadioButton)control);
                    if (radio.IsChecked)
                    {
                        var question = radio.AutomationId.Split('|');

                        respostas.QuestionId = Guid.Parse(question[0]);
                        respostas.QuestionTitle = question[1];

                        var answer = radio.ClassId.Split('|');

                        respostas.AnswerOptionId = Guid.Parse(answer[0]);
                        respostas.AnswerText = radio.Content.ToString();
                        respostas.AnswerValue = Convert.ToDecimal(answer[1]);

                        ListOfAnswers.Add(respostas);
                    }
                }

                if (control is Grid)
                {
                    foreach (var item in ((Grid)control).Children)
                    {
                        if (item is CheckBox)
                        {
                            PatientAnswersViewModel respostas = new PatientAnswersViewModel();
                            var check = ((CheckBox)item);
                            if (check.IsChecked)
                            {
                                var question = check.AutomationId.Split('|');

                                respostas.QuestionId = Guid.Parse(question[0]);
                                respostas.QuestionTitle = question[1];

                                var answer = check.ClassId.Split('|');

                                respostas.AnswerOptionId = Guid.Parse(answer[0]);
                                respostas.AnswerText = answer[2];
                                respostas.AnswerValue = Convert.ToDecimal(answer[1]);

                                ListOfAnswers.Add(respostas);
                            }
                        }
                    }

                }

            }

            foreach (var item in ListOfAnswers)
            {
                item.InquiryId = InquiryId;
                item.InquiryTitle = InquiryTitle;
                item.PatientId = PatientId;
                item.InquiryScheduleId = InquiryScheduleId;

                if (string.IsNullOrEmpty(item.CriteriaGroupName) || string.IsNullOrEmpty(item.AlertCssClass))
                {
                    item.AlertCssClass = string.Empty;
                    item.CriteriaGroupName = string.Empty;
                }

                var c4 = item.QuestionId;
                var c6 = item.QuestionTitle;
                var c7 = item.AnswerOptionId;
                var c8 = item.AnswerText;
                var c9 = item.AnswerValue;

                answered = await ApiService.Post<PatientAnswersViewModel>("patient-answers", item);
            }

            await setAnsweredToTrue(InquiryScheduleId);

        }
    }
}
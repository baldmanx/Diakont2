using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Input;

namespace Diakont2
{
    class DiakontViewModel : BaseViewModel
    {
        private DiakontEntities context;

        private static DataGrid jobsDataGrid = null;
        private static DataGrid departmentsDataGrid = null;
        private static DataGrid reportsDataGrid = null;

        private List<string> _dep_names { get; set; }
        public List<string> Dep_names
        {
            get
            {
                return _dep_names;
            }
            set
            {
                _dep_names = value;
                NotifyPropertyChanged(nameof(Dep_names));
            }
        }

        private List<string> _job_names { get; set; }
        public List<string> Job_names
        {
            get
            {
                return _job_names;
            }
            set
            {
                _job_names = value;
                NotifyPropertyChanged(nameof(Job_names));
            }
        }

        private List<string> _djob_names { get; set; }
        public List<string> DJob_names
        {
            get
            {
                return _djob_names;
            }
            set
            {
                _djob_names = value;
                NotifyPropertyChanged(nameof(DJob_names));
            }
        }

        private List<string> _string_dates { get; set; }
        public List<string> String_dates
        {
            get
            {
                return _string_dates;
            }
            set
            {
                _string_dates = value;
                NotifyPropertyChanged(nameof(String_dates));
            }
        }

        private List<string> _rstring_dates { get; set; }
        public List<string> RString_dates
        {
            get
            {
                return _rstring_dates;
            }
            set
            {
                _rstring_dates = value;
                NotifyPropertyChanged(nameof(RString_dates));
            }
        }

        private string _fee { get; set; }
        public string Fee
        {
            get
            {
                return _fee;
            }
            set
            {
                _fee = value;
                NotifyPropertyChanged(nameof(Fee));
            }
        }

        private string _workers_amount { get; set; }
        public string Workers_amount
        {
            get
            {
                return _workers_amount;
            }
            set
            {
                _workers_amount = value;
                NotifyPropertyChanged(nameof(Workers_amount));
            }
        }

        private DateTime _jdate_from { get; set; }
        public DateTime JDate_from
        {
            get
            {
                return _jdate_from;
            }
            set
            {
                _jdate_from = value;
                NotifyPropertyChanged(nameof(JDate_from));
            }
        }

        private DateTime _date_to { get; set; }
        public DateTime Date_to
        {
            get
            {
                return _date_to;
            }
            set
            {
                _date_to = value;
                NotifyPropertyChanged(nameof(Date_to));
            }
        }

        private DateTime _rjd_from { get; set; }
        public DateTime Rjd_from
        {
            get
            {
                return _rjd_from;
            }
            set
            {
                _rjd_from = value;
                NotifyPropertyChanged(nameof(Rjd_from));
            }
        }

        private DateTime _rdate_to { get; set; }
        public DateTime RDate_to
        {
            get
            {
                return _rdate_to;
            }
            set
            {
                _rdate_to = value;
                NotifyPropertyChanged(nameof(RDate_to));
            }
        }

        private string _selected_job { get; set; }
        public string Selected_job
        {
            get
            {
                return _selected_job;
            }
            set
            {
                _selected_job = value;
                NotifyPropertyChanged(nameof(Selected_job));
            }
        }
        private string _dselected_job { get; set; }
        public string DSelected_job
        {
            get
            {
                return _dselected_job;
            }
            set
            {
                _dselected_job = value;
                //DatesSource();
                NotifyPropertyChanged(nameof(DSelected_job));
            }
        }

        private int _dselected_job_index { get; set; }
        public int DSelected_job_index
        {
            get
            {
                return _dselected_job_index;
            }
            set
            {
                _dselected_job_index = value;
                DatesSource();
                NotifyPropertyChanged(nameof(DSelected_job_index));
            }
        }

        private string _rdselected_dep { get; set; }
        public string RDSelected_dep
        {
            get
            {
                return _rdselected_dep;
            }
            set
            {
                _rdselected_dep = value;
                RDatesSource();
                NotifyPropertyChanged(nameof(RDSelected_dep));
            }
        }
        
        private string _rdSelected_date_to { get; set; }
        public string RDSelected_date_to
        {
            get
            {
                return _rdSelected_date_to;
            }
            set
            {
                _rdSelected_date_to = value;
                NotifyPropertyChanged(nameof(RDSelected_date_to));
            }
        }

        private int _rdselected_dep_index { get; set; }
        public int RDSelected_dep_index
        {
            get
            {
                return _rdselected_dep_index;
            }
            set
            {
                _rdselected_dep_index = value;
                RDatesSource();
                NotifyPropertyChanged(nameof(RDSelected_dep_index));
            }
        }

        private string _selected_jdate { get; set; }
        public string Selected_jdate
        {
            get
            {
                return _selected_jdate;
            }
            set
            {
                _selected_jdate = value;
                NotifyPropertyChanged(nameof(Selected_jdate));
            }
        }

        private string _rselected_jdate { get; set; }
        public string RSelected_jdate
        {
            get
            {
                return _rselected_jdate;
            }
            set
            {
                _rselected_jdate = value;
                NotifyPropertyChanged(nameof(RSelected_jdate));
            }
        }

        private string _selected_department { get; set; }
        public string Selected_department
        {
            get
            {
                return _selected_department;
            }
            set
            {
                _selected_department = value;
                NotifyPropertyChanged(nameof(Selected_department));
            }
        }

        private int _selected_department_index { get; set; }
        public int Selected_department_index
        {
            get
            {
                return _selected_department_index;
            }
            set
            {
                _selected_department_index = value;
                JobsSource();
                NotifyPropertyChanged(nameof(Selected_department_index));
            }
        }

        public ICommand AddJob { get; set; }
        public ICommand AddDepartment { get; set; }
        public ICommand ShowReport { get; set; }

        public void AddNewJob()
        {
            Jobs job = new Jobs();
            var jobs = context.Jobs.ToList();

            job.job_name = Selected_job;
            job.jdate_from = JDate_from.Date;
            job.fee = decimal.Parse(Fee);

            context.Jobs.Add(job);

            context.Jobs.Load();
            context.SaveChanges();

            jobsDataGrid.ItemsSource = context.Jobs.SqlQuery("select * from Jobs where fee is not null").ToList();
        }

        public void AddNewDepartment()
        {
            Jobs job = new Jobs();
            Departments department = new Departments();

            var jobs = context.Jobs.ToList();
            var departments = context.Departments.ToList();

            decimal job_fee = 0;
            int workers_amount = 0;

            department.department_name = Selected_department;
            department.jd_from = Convert.ToDateTime(Selected_jdate);
            department.date_to = Date_to.Date;
            department.workers_amount = Convert.ToInt32(Workers_amount);

            foreach (Jobs j in context.Jobs)
            {
                if (j.jdate_from == department.jd_from)
                {
                    job_fee = (decimal)j.fee;
                    workers_amount = (int)department.workers_amount;
                }
            }

            int months_diff;

            if (department.date_to >= department.jd_from)
            {
                months_diff = department.date_to.Month - department.jd_from.Month;
            }
            else
            {
                months_diff = department.jd_from.Month - department.date_to.Month;
            }
            

            if (months_diff > 0)
            {
                department.monthly_pay = job_fee * months_diff * workers_amount;
            }
            else
            {
                department.monthly_pay = job_fee * workers_amount;
            }

            context.Departments.Add(department);

            context.Departments.Load();
            context.SaveChanges();

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-7442FLF;Initial Catalog=Diakont; Integrated security = true; User ID=DESKTOP-7442FLF\dehnk;Password=1803"))
            {
                connection.Open();

                string sqlQuery = "select department_name, job_name, jd_from, date_to, workers_amount from Departments join Jobs on Departments.jd_from = Jobs.jdate_from where workers_amount is not null";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Departments");
                da.Fill(dt);

                departmentsDataGrid.ItemsSource = dt.DefaultView;
                da.Update(dt);

                connection.Close();
            }
        }

        public void ShowPaymentReport()
        {
            /*using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-7442FLF;Initial Catalog=Diakont; Integrated security = true; User ID=DESKTOP-7442FLF\dehnk;Password=1803"))
            {
                connection.Open();

                string sqlQuery = "select(select department_name from Departments where jd_from = '" + RDSelected_dep + "') as department_name, (select jd_from from Departments where jd_from = '" + Rjd_from.Date + "') as jd_from,(select date_to from Departments where date_to = '" + RDate_to.Date + "') as date_to,(select SUM(monthly_pay) as monthly_pay from Departments where jd_from >= '" + Rjd_from.Date + "' and jd_from < '" + RDate_to.Date + "' and date_to > '" + Rjd_from.Date + "' and date_to <= '" + RDate_to.Date + "') as monthly_pay";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Departments");
                da.Fill(dt);

                reportsDataGrid.ItemsSource = dt.DefaultView;
                da.Update(dt);

                connection.Close();
            }*/            

            context.Departments.Load();
            context.SaveChanges();
        }

        public enum DIndex : int
        {
            kb = 0, to = 1
        }
        public void JobsSource()
        {
            switch (Selected_department_index)
            {
                case (int)DIndex.kb:
                    {
                        DJob_names.Clear();
                        DJob_names.Add(Job_names[0]);
                        DSelected_job = DJob_names[0];
                        DSelected_job_index = 0;
                        break;
                    }

                case (int)DIndex.to:
                    {
                        DJob_names.Clear();
                        DJob_names.Add(Job_names[1]);
                        DSelected_job = DJob_names[0];
                        DSelected_job_index = 1;
                        break;
                    }
            }
            NotifyPropertyChanged(nameof(DJob_names));
            NotifyPropertyChanged(nameof(DSelected_job));
            NotifyPropertyChanged(nameof(DSelected_job_index));
        }

        public enum JIndex : int
        {
            ik = 0, it = 1
        }
        public void DatesSource()
        {
            switch (DSelected_job_index)
            {
                case (int)JIndex.ik:
                    {
                        String_dates.Clear();
                        context = new DiakontEntities();
                        context.Jobs.Load();
                        
                        foreach (Jobs j in context.Jobs)
                        {
                            if (j.job_name == "Инженер-конструктор" && j.fee != null)
                            {
                                String_dates.Add(j.jdate_from.ToString());
                            }
                        }
                        if (String_dates.LongCount() > 0)
                        {
                            Selected_jdate = String_dates[0];
                            RSelected_jdate = String_dates[0];
                        }
                        break;
                    }

                case (int)JIndex.it:
                    {
                        String_dates.Clear();
                        context = new DiakontEntities();
                        context.Jobs.Load();

                        foreach (Jobs j in context.Jobs)
                        {
                            if (j.job_name == "Инженер-технолог" && j.fee != null)
                            {
                                String_dates.Add(j.jdate_from.ToString());
                            }
                        }
                        Selected_jdate = String_dates[0];
                        RSelected_jdate = String_dates[0];
                        break;
                    }
            }
            NotifyPropertyChanged(nameof(String_dates));
            NotifyPropertyChanged(nameof(Selected_jdate));
            NotifyPropertyChanged(nameof(RSelected_jdate));
        }

        public void RDatesSource()
        {
            switch (RDSelected_dep_index)
            {
                case (int)DIndex.kb:
                    {
                        String_dates.Clear();
                        RString_dates.Clear();
                        context = new DiakontEntities();
                        context.Departments.Load();

                        foreach (Departments d in context.Departments)
                        {
                            if (d.department_name == "КБ" && d.workers_amount != null)
                            {
                                String_dates.Add(d.jd_from.ToString());
                                RString_dates.Add(d.date_to.ToString());
                            }
                        }

                        if (RString_dates.LongCount() > 0)
                        {
                            RSelected_jdate = RString_dates[0];
                        }
                        break;
                    }

                case (int)DIndex.to:
                    {
                        String_dates.Clear();
                        RString_dates.Clear();
                        context = new DiakontEntities();
                        context.Departments.Load();

                        foreach (Departments d in context.Departments)
                        {
                            if (d.department_name == "ТО" && d.workers_amount != null)
                            {
                                String_dates.Add(d.jd_from.ToString());
                                RString_dates.Add(d.date_to.ToString());
                            }
                        }

                        RSelected_jdate = RString_dates[0];
                        break;
                    }
            }
            NotifyPropertyChanged(nameof(String_dates));
            NotifyPropertyChanged(nameof(RString_dates));
            NotifyPropertyChanged(nameof(RSelected_jdate));
        }

        public DiakontViewModel()
        {
            context = new DiakontEntities();
            Job_names = new List<string>();
            DJob_names = new List<string>();
            Dep_names = new List<string>();
            String_dates = new List<string>();
            RString_dates = new List<string>();

            context.Jobs.Load();
            context.Departments.Load();

            var init_jobs = context.Jobs.ToList();
            var init_departments = context.Departments.ToList();

            foreach (Jobs j in init_jobs)
            {
                if (j.fee == null)
                {
                    Job_names.Add(j.job_name);
                }
            }

            foreach (Departments d in init_departments)
            {
                if (d.workers_amount == null)
                {
                    Dep_names.Add(d.department_name);
                }
            }

            foreach (Departments d in init_departments)
            {
                if (d.workers_amount != null)
                {
                    RString_dates.Add(d.date_to.ToString());
                }
            }

            foreach (Jobs j in init_jobs)
            {
                if (j.job_name == "Инженер-конструктор" && j.fee != null)
                {
                    String_dates.Add(j.jdate_from.ToString());
                }
            }            

            Selected_job = Job_names[0];
            Selected_department = Dep_names[0];
            Selected_department_index = 0;
            RDSelected_dep_index = 0;

            JDate_from = DateTime.Now;
            Date_to = DateTime.Now;
            Rjd_from = DateTime.Now;
            RDate_to = DateTime.Now;

            jobsDataGrid.ItemsSource = context.Jobs.SqlQuery("select * from Jobs where fee is not null").ToList();

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-7442FLF;Initial Catalog=Diakont; Integrated security = true; User ID=DESKTOP-7442FLF\dehnk;Password=1803"))
            {
                connection.Open();

                string sqlQuery = "select department_name, job_name, jd_from, date_to, workers_amount from Departments join Jobs on Departments.jd_from = Jobs.jdate_from where workers_amount is not null";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Departments");
                da.Fill(dt);

                departmentsDataGrid.ItemsSource = dt.DefaultView;
                da.Update(dt);

                connection.Close();
            }

            AddJob = new BaseCommand(AddNewJob, true);
            AddDepartment = new BaseCommand(AddNewDepartment, true);
            ShowReport = new BaseCommand(ShowPaymentReport, true);
        }



        public static readonly DependencyProperty jobsDataGridProperty = 
                                           DependencyProperty.RegisterAttached("JobsDataGrid", typeof(DataGrid),
                                           typeof(DiakontViewModel),
                                           new FrameworkPropertyMetadata(OnJobsDataGridChanged));
        public static void SetJobsDataGrid(DependencyObject element, DataGrid value)
        {
            element.SetValue(jobsDataGridProperty, value);
        }

        public static DataGrid GetJobsDataGrid(DependencyObject element)
        {
            return (DataGrid)element.GetValue(jobsDataGridProperty);
        }

        public static void OnJobsDataGridChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            jobsDataGrid = obj as DataGrid;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static readonly DependencyProperty departmentsDataGridProperty =
                                     DependencyProperty.RegisterAttached("DepartmentsDataGrid", typeof(DataGrid),
                                     typeof(DiakontViewModel),
                                     new FrameworkPropertyMetadata(OnDepartmentsDataGridChanged));
        public static void SetDepartmentsDataGrid(DependencyObject element, DataGrid value)
        {
            element.SetValue(departmentsDataGridProperty, value);
        }

        public static DataGrid GetDepartmentsDataGrid(DependencyObject element)
        {
            return (DataGrid)element.GetValue(departmentsDataGridProperty);
        }

        public static void OnDepartmentsDataGridChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            departmentsDataGrid = obj as DataGrid;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static readonly DependencyProperty reportsDataGridProperty =
                                     DependencyProperty.RegisterAttached("ReportsDataGrid", typeof(DataGrid),
                                     typeof(DiakontViewModel),
                                     new FrameworkPropertyMetadata(OnReportsDataGridChanged));
        public static void SetReportsDataGrid(DependencyObject element, DataGrid value)
        {
            element.SetValue(reportsDataGridProperty, value);
        }

        public static DataGrid GetReportsDataGrid(DependencyObject element)
        {
            return (DataGrid)element.GetValue(reportsDataGridProperty);
        }

        public static void OnReportsDataGridChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            reportsDataGrid = obj as DataGrid;
        }

    }
}

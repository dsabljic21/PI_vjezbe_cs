﻿using Evaluation_Manager_DS.Models;
using Evaluation_Manager_DS.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evaluation_Manager_DS {
    public partial class FrmEvaluation : Form {
        private Student student;

        public Student SelectedStudent { get; private set; }

        public FrmEvaluation(Models.Student selectedStudent) {
            InitializeComponent();
            student = selectedStudent;
        }

        private void FrmEvaluation_Load(object sender, EventArgs e) {
            SetFormText();
            var activities = ActivityRepository.GetActivities();
            cboActivities.DataSource = activities;
        }

        private void SetFormText() {
            Text = student.FirstName + " " + student.LastName;
        }

        private void cboActivities_SelectedIndexChanged(object sender, EventArgs e) {
            var currentActivity = cboActivities.SelectedItem as Activity;
            txtActivityDescription.Text = currentActivity.Description;
            txtMinForGrade.Text = currentActivity.MinPointsForGrade + "/" +
           currentActivity.MaxPoints;
            txtMinForSignature.Text = currentActivity.MinPointsForSignature + "/" +
           currentActivity.MaxPoints;
            // numPoints.Minimum = 0;
            // numPoints.Maximum = currentActivity.MaxPoints;
            var evaluation =
 EvaluationRepository.GetEvaluation(SelectedStudent, currentActivity);
            if (evaluation != null) {
                txtTeacher.Text = evaluation.Evaluator.ToString();
                txtEvaluationDate.Text = evaluation.EvaluationDate.ToString();
                numPoints.Value = evaluation.Points;
            } else {
                txtTeacher.Text = FrmLogin.LoggedTeacher.ToString();
                txtEvaluationDate.Text = "-";
                numPoints.Value = 0;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }
    }
}

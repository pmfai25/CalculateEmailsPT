﻿using System;
using Autofac;
using CalculateEmails.Autofac;
using CalculateEmails.Contract.DataContract;
using CalculateEmails.WCFService;
using CalculateEmails.WCFService.Application;
using CalculateEmails.Configuration;
using DALContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests
{
    [TestClass]
    public class MailTests
    {
        string MailCountAdd = "MailCountAdd";
        string MailCountProcessed = "MailCountProcessed";
        string Sent = "Sent";
        string TaskCountAdded = "TaskCountAdded";
        string TaskCountFinished = "TaskCountFinished";
        string TaskCountRemoved = "TaskCountRemoved";


      

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {

        }

        [TestInitialize()]
        public void Initialize()
        {
        }

        [TestCleanup()]
        public void Cleanup()
        {
            new DBSetup().TruncateTable();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            new DBSetup().DropDatabase();
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
        }


        [TestMethod]
        public void MailReferenceMethod()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void MailMailOneNewMail()
        {
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Added, InboxType.Main);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(1, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }


        [TestMethod]
        public void SentOneMail()
        {
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Added, InboxType.Sent);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(x.MailCountAdd, 0, MailCountAdd);
            Assert.AreEqual(x.MailCountProcessed, 0, MailCountProcessed);
            Assert.AreEqual(x.MailCountSent, 1);
            Assert.AreEqual(x.TaskCountAdded, 0);
            Assert.AreEqual(x.TaskCountFinished, 0);
            Assert.AreEqual(x.TaskCountRemoved, 0);
        }

        [TestMethod]
        public void MailMoveMailBetweenInboxesReverseOrder()
        {
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox);
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }

        [TestMethod]
        public void Mail()
        {
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }

        [TestMethod]
        public void MailMove4MailBetweenInboxesRightOrder()
        {
            BaseManager.WriteToLog("Move 4 mails between inboxes");
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox);
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent, Sent);
            Assert.AreEqual(0, x.TaskCountAdded, TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished, TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved, TaskCountRemoved);
        }

        [TestMethod]
        public void MailMove6MailBetweenInboxesRightOrder()
        {
            BaseManager.WriteToLog("Move 4 mails between inboxes");
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox);
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent, Sent);
            Assert.AreEqual(0, x.TaskCountAdded, TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished, TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved, TaskCountRemoved);
        }

        [TestMethod]
        public void MailProcessOneMailFromMainInbox()
        {
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(1, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }


        [TestMethod]
        public void MailProcessTwoMailFromSubInbox()
        {
            BaseManager.WriteToLog("ProcessTwoMailFromSubInbox");
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Subinbox);
            bLManager.Process(EmailActionType.Removed, InboxType.Subinbox);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(0, x.MailCountAdd,MailCountAdd);
            Assert.AreEqual(2, x.MailCountProcessed,MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent,Sent);
            Assert.AreEqual(0, x.TaskCountAdded,TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished,TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved,TaskCountRemoved);
        }

    }
}
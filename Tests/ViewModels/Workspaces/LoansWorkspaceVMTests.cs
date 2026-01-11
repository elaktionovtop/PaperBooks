using PaperBooks.Models;
using PaperBooks.Services;

using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ViewModels.Workspaces
{
    public sealed class LoansWorkspaceVMTests
    {
        [Fact]
        public void Load_loans_when_reader_selected()
        {
            var reader1 = new Reader { Name = "R1" };
            var reader2 = new Reader { Name = "R2" };

            var loans = new[]
            {
                new Loan { Reader = reader1 },
                new Loan { Reader = reader1 },
                new Loan { Reader = reader2 }
            };

            var service = new InMemoryLoansService(loans);
            var vm = new LoansWorkspaceVM(service);

            vm.CurrentReader = reader1;

            Assert.Equal(2, vm.Loans.Count);
            Assert.All(vm.Loans, loan => Assert.Equal(reader1, loan.Reader));
            Assert.NotNull(vm.CurrentLoan);
            Assert.Equal(vm.Loans.First(), vm.CurrentLoan);
        }

        [Fact]
        public void Reader_without_loans_results_in_empty_list()
        {
            var readerWithLoans = new Reader { Name = "R1" };
            var readerWithoutLoans = new Reader { Name = "R2" };

            var loans = new[]
            {
                new Loan { Reader = readerWithLoans }
            };

            var service = new InMemoryLoansService(loans);
            var vm = new LoansWorkspaceVM(service);

            vm.CurrentReader = readerWithoutLoans;

            Assert.Empty(vm.Loans);
            Assert.Null(vm.CurrentLoan);
        }

        [Fact]
        public void Reset_reader_clears_loans_and_current()
        {
            var reader = new Reader { Name = "R1" };

            var loans = new[]
            {
                new Loan { Reader = reader }
            };

            var service = new InMemoryLoansService(loans);
            var vm = new LoansWorkspaceVM(service);

            vm.CurrentReader = reader;
            vm.CurrentReader = null;

            Assert.Empty(vm.Loans);
            Assert.Null(vm.CurrentLoan);
        }
    }
}

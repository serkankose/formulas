/// <reference types="cypress" />

describe('app navigation', () => {

    beforeEach(() => {
        cy.visit('https://localhost:7296/');
    });

    it('can browse to all pages', () => {

        cy.get('.nav-link')
            .each(($el, index, $list) => {
                if (index > 1) {
                    cy.wrap($el)
                        .click()
                        .get('.content')
                        .should('contain.text', $el.text().trim());
                }
            });

    });

    it('Banking contains CalcBalloonLoanPayment', () => {

        cy.get('.nav-link')
            .each(($el, index, $list) => {
                cy.wrap($el).click();
            });

        cy.get(':nth-child(2) > .nav-link')
            .click()
            .contains('Banking');


        cy.get(':nth-child(2) > .m-0')
            .contains('CalcBalloonLoanPayment');


    });
});

describe('can calculate', () => {
    beforeEach(() => {
        cy.visit('https://localhost:7296/');
    });

    it('can calculate Banking.BalloonLoanPayment', () => {
        cy.get(':nth-child(2) > .nav-link')
            .click()
            .contains('Banking');

        cy.get(':nth-child(2) > .m-0')
            .contains('CalcBalloonLoanPayment');

        cy.get('#CalcBalloonLoanPayment_presentValue')
            .type('10000');

        cy.get('#CalcBalloonLoanPayment_balloonAmount')
            .type('90000');

        cy.get('#CalcBalloonLoanPayment_ratePerPeriod')
            .type('3');

        cy.get('#CalcBalloonLoanPayment_numberOfPeriods')
            .type('7');


        cy.get('#CalcBalloonLoanPayment_result')
            .should('have.text', '29985.350668375755356161875115');

    });
});

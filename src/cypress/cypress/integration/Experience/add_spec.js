import * as common from '../Helper/CommonHelper';

describe('experience', () => {
    context('adding', () => {
        it('can reach the add experience page via the /add route', () => {
            cy.visit(`${common.baseUrl}/add`)
            cy.title().should('include', 'Add Exprience')
        })
    })
})
import * as common from '../Helper/CommonHelper';

describe('experience', () => {
    context('adding', () => {
        it('can reach the add experience page via the / route', () => {
            cy.visit(`${common.baseUrl}/`)
            cy.get('textarea').should('have.attr', 'placeholder', 'record an experience')
        })
    })
})